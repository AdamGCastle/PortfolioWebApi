using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebApi.Contexts;
using PortfolioWebApi.Extensions;
using PortfolioWebApi.Models;

namespace PortfolioWebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [EnableCors]
    public class SurveysController : ControllerBase
    {
        internal AcPortFolioDbContext _acPortfolioDb = new ();

        [HttpPost]
        public async Task<IActionResult> Create(Survey model)
        {
            if(string.IsNullOrWhiteSpace(model.Name))
            {
                return BadRequest("Please enter a survey name.");
            }

            if(model.Questions.Count == 0)
            {
                return BadRequest("Please add a question.");
            }

            var accountId = User.GetAccountIdOrNull();

            if (accountId.HasValue && accountId != 0)
            {               
                model.CreatedByAccountId = accountId;                
            }          

            model.Id = 0;
            model.DateCreated = DateTime.Now;

            foreach (var question in model.Questions)
            {
                question.DateCreated = DateTime.Now;

                foreach (var option in question.MultipleChoiceOptions)
                {
                    option.DateCreated = DateTime.Now;
                }
            }

            _acPortfolioDb.Surveys.Add(model);
            await _acPortfolioDb.SaveChangesAsync();

            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> ReadAll(bool forEdit)
        {
            var accountId = User.GetAccountIdOrNull();
            var surveys = await _acPortfolioDb.Surveys
                                .Where(x => !forEdit || x.CreatedByAccountId == 0 || x.CreatedByAccountId == null || x.CreatedByAccountId == accountId)
                                .ToListAsync();

            return Ok(surveys);
        }

        [HttpGet]
        public async Task<IActionResult> ReadById(int id)
        {
            if (id == 0)
            {
                return BadRequest("Survey ID cannot be 0.");
            }

            return Ok(await _acPortfolioDb.Surveys
                .Include(x => x.Questions)
                .ThenInclude(x => x.MultipleChoiceOptions)
                .FirstOrDefaultAsync(x => x.Id == id));
        }

        [HttpGet]
        public async Task<IActionResult> ReadByAccountId(int accountId)
        {
            if (accountId == 0)
            {
                return BadRequest("Account ID cannot be 0.");
            }

            var loggedInAccountId = User.GetAccountIdOrNull();

            return Ok( _acPortfolioDb.Surveys
                .Include(x => x.Questions)
                .ThenInclude(x => x.MultipleChoiceOptions)
                .Where(x => x.CreatedByAccountId == loggedInAccountId));
        }

        [HttpPut] 
        public async Task<IActionResult> SubmitSurveyResponse(SurveyResponse model)
        {
            if (model.SurveyId == 0)
            {
                return BadRequest("Invalid Survey ID.");
            }

            var dbSurvey = await _acPortfolioDb.Surveys.FirstOrDefaultAsync(x => x.Id == model.SurveyId);

            if (dbSurvey == null)
            {
                return BadRequest("Invalid Survey ID.");
            }

            if (model.QuestionResponses.Count == 0)
            {
                return BadRequest("No responses found.");
            }                        

            model.DateCreated = DateTime.Now;
            model.Id = 0;

            var dbQuestions = _acPortfolioDb.Questions
                .Include(x => x.MultipleChoiceOptions)
                .Where(x => x.SurveyId == model.SurveyId);

            foreach (var questionResponse in model.QuestionResponses)
            {
                questionResponse.Id = 0;

                var dbQuestion = await dbQuestions.FirstOrDefaultAsync(x => x.Id == questionResponse.QuestionId);

                if (dbQuestion == null || questionResponse.MultipleChoiceOptionResponses == null)
                {
                    return BadRequest("Invalid response data.");
                }

                foreach (var multipleChoiceOptionResponse in questionResponse.MultipleChoiceOptionResponses)
                {
                    var multiplChoiceOptionExists = dbQuestion.MultipleChoiceOptions.Any(x => x.Id == multipleChoiceOptionResponse.MultipleChoiceOptionId);
                    if (!multiplChoiceOptionExists)
                    {
                        return BadRequest("Invalid response data.");
                    }
                }
            }

            _acPortfolioDb.SurveyResponses.Add(model);
            await _acPortfolioDb.SaveChangesAsync();

            dbSurvey = await _acPortfolioDb.Surveys
                .Include(x => x.SurveyResponses).ThenInclude(x => x.QuestionResponses).ThenInclude(x => x.MultipleChoiceOptionResponses)
                .FirstOrDefaultAsync(x => x.Id == model.SurveyId);

            return Ok(dbSurvey);           
        }

        [HttpPut]
        public async Task<IActionResult> Update(Survey model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return BadRequest("Please enter a survey name.");
            }

            if (model.Questions.Count == 0)
            {
                return BadRequest("Please add a question.");
            }

            var dbSurvey = await _acPortfolioDb.Surveys
                .Include(x => x.Questions)
                .ThenInclude(x => x.MultipleChoiceOptions)
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (dbSurvey == null)
            {
                return BadRequest("Invalid survey ID.");
            }

            if (dbSurvey.CreatedByAccountId.HasValue && dbSurvey.CreatedByAccountId != 0)
            {
                var accountId = User.GetAccountIdOrNull();

                if (accountId != dbSurvey.CreatedByAccountId)
                {
                    return Unauthorized("Invalid authorisation token. This survey can only be edited by the user who created it.");
                }
            }

            dbSurvey.Name = model.Name;

            foreach (var question in model.Questions)
            {
                var dbQuestion = dbSurvey.Questions.FirstOrDefault(q => q.Id == question.Id)
                                ?? dbSurvey.Questions.FirstOrDefault(q => q.SurveyId == dbSurvey.Id && q.Text == question.Text);

                if (dbQuestion == null)
                {
                    question.DateCreated = DateTime.Now;
                    dbSurvey.Questions.Add(question);

                    continue;
                }

                if (question.Removed)
                {
                    var questionResponses = _acPortfolioDb.QuestionResponses
                        .Include(x => x.MultipleChoiceOptionResponses)
                        .Where(x => x.QuestionId == dbQuestion.Id)
                        .ToList();

                    foreach (var questionResponse in questionResponses)
                    {
                        _acPortfolioDb.MultipleChoiceOptionResponses.RemoveRange(questionResponse.MultipleChoiceOptionResponses);
                    }

                    _acPortfolioDb.QuestionResponses.RemoveRange(dbQuestion.QuestionResponses);
                    _acPortfolioDb.MultipleChoiceOptions.RemoveRange(dbQuestion.MultipleChoiceOptions);

                    dbSurvey.Questions.Remove(dbQuestion);

                    continue;
                }

                foreach (var removedMco in question.MultipleChoiceOptions.Where(x => x.Removed).ToList())
                {
                    var dbMultipleChoiceOption = await _acPortfolioDb.MultipleChoiceOptions
                                                .Include(x => x.MultipleChoiceOptionResponses)
                                                .FirstOrDefaultAsync(x => x.Id == removedMco.Id);

                    _acPortfolioDb.MultipleChoiceOptionResponses.RemoveRange(dbMultipleChoiceOption.MultipleChoiceOptionResponses);
                    _acPortfolioDb.MultipleChoiceOptions.Remove(dbMultipleChoiceOption);
                }

                dbQuestion.DateUpdated = DateTime.Now;
                dbQuestion.Text = question.Text;
                dbQuestion.IsMultipleChoice = question.IsMultipleChoice;
                dbQuestion.MultipleAnswersPermitted = question.MultipleAnswersPermitted;

                foreach(var multipleChoiceOption in question.MultipleChoiceOptions)
                {
                    var dbMultipleChoiceOption = dbQuestion.MultipleChoiceOptions.FirstOrDefault(m => m.Id == multipleChoiceOption.Id)
                                ?? (dbQuestion.MultipleChoiceOptions.FirstOrDefault(m => m.Text == multipleChoiceOption.Text));

                    if (dbMultipleChoiceOption == null)
                    {
                        dbQuestion.MultipleChoiceOptions.Add(multipleChoiceOption);

                        continue;
                    }

                    dbMultipleChoiceOption.DateUpdated = DateTime.Now;
                }
            }

            dbSurvey.DateUpdated = DateTime.Now;

            foreach(var question in model.Questions.Where(x => !x.Removed).ToList())
            {
                if (question.Id > 0)
                {
                    question.DateUpdated = DateTime.Now;
                }
                else
                {
                    question.DateCreated = DateTime.Now;
                }

                foreach(var option in question.MultipleChoiceOptions)
                {
                    if (option.Id > 0)
                    {
                        option.DateUpdated = DateTime.Now;
                    }
                    else
                    {
                        option.DateCreated = DateTime.Now;
                    }
                }
            }

            await _acPortfolioDb.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid survey ID");
            }

            var dbSurvey = await _acPortfolioDb.Surveys.Include(s => s.Questions).
                ThenInclude(x => x.MultipleChoiceOptions).FirstOrDefaultAsync(x => x.Id == id);

            if (dbSurvey == null)
            {
                return BadRequest("Invalid survey ID");
            }

            if (dbSurvey.CreatedByAccountId != null && dbSurvey.CreatedByAccountId != 0)
            {
                if (dbSurvey.CreatedByAccountId.HasValue && dbSurvey.CreatedByAccountId != 0)
                {
                    var accountId = User.GetAccountIdOrNull();

                    if (accountId != dbSurvey.CreatedByAccountId)
                    {
                        return Unauthorized("Invalid authorisation token. This survey can only be deleted by the user who created it.");
                    }
                }
            }

            foreach (var question in dbSurvey.Questions)
            {
                var dbQuestion = await _acPortfolioDb.Questions.FirstOrDefaultAsync(x => x.Id == question.Id);

                _acPortfolioDb.MultipleChoiceOptions.RemoveRange(dbQuestion.MultipleChoiceOptions);
            }

            _acPortfolioDb.Questions.RemoveRange(dbSurvey.Questions);
            _acPortfolioDb.Surveys.Remove(dbSurvey);

            await _acPortfolioDb.SaveChangesAsync();

            return Ok();
        }
    }
}