using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioWebApi.Contexts;
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

            await _acPortfolioDb.Surveys.AddAsync(model);

            await _acPortfolioDb.SaveChangesAsync();

            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> ReadAll()
        {
            var toretun = _acPortfolioDb.Surveys;

            //return Ok(_acPortfolioDb.Surveys);
            return Ok(toretun);
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

            var dbSurvey = await _acPortfolioDb.Surveys.FirstOrDefaultAsync(x => x.Id == model.Id);

            dbSurvey.Name = model.Name;
            dbSurvey.Questions.Clear();
            dbSurvey.Questions.AddRange(model.Questions);
            dbSurvey.DateUpdated = DateTime.Now;

            foreach(var question in model.Questions)
            {
                question.DateUpdated = DateTime.Now;

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