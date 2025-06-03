using Application.CQRS.Commands;
using Application.CQRS.Queries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.CQRS.DTOs;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserCourseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserCourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all courses purchased by the user
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<FinancialCourse>>> GetUserCourses(string userId)
        {
            var query = new GetUserCoursesQuery { UserId = userId };
            var courses = await _mediator.Send(query);
            return Ok(courses);
        }

        /// <summary>
        /// Purchase a course for a user
        /// </summary>
        [HttpPost("purchase")]
        public async Task<ActionResult<string>> PurchaseCourse([FromBody] PurchaseCourseCommand command)
        {
            var purchaseId = await _mediator.Send(command);
            return Ok(new { Id = purchaseId });
        }

        /// <summary>
        /// Mark a course item as completed
        /// </summary>
        [HttpPost("complete-item")]
        public async Task<ActionResult<bool>> MarkCourseItemCompleted([FromBody] MarkCourseItemCompletedCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Get all available courses (not purchased by the user)
        /// </summary>
        [HttpGet("available/{userId}")]
        public async Task<ActionResult<IEnumerable<FinancialCourse>>> GetAvailableCourses(string userId)
        {
            // First get all courses
            var allCoursesQuery = new GetAllCoursesQuery();
            var allCourses = await _mediator.Send(allCoursesQuery);

            // Then get user's purchased courses
            var userCoursesQuery = new GetUserCoursesQuery { UserId = userId };
            var userCourses = await _mediator.Send(userCoursesQuery);

            // Filter out courses the user already has
            var userCourseIds = userCourses.Select(c => c.Id).ToHashSet();
            var availableCourses = allCourses.Where(c => !userCourseIds.Contains(c.Id)).ToList();

            return Ok(availableCourses);
        }

        /// <summary>
        /// Get user's progress for a specific course
        /// </summary>
        [HttpGet("progress/{userId}/{courseId}")]
        public async Task<ActionResult<UserCourseProgressDto>> GetCourseProgress(string userId, string courseId)
        {
            var query = new GetUserCourseProgressQuery 
            { 
                UserId = userId,
                CourseId = courseId
            };
            
            var progress = await _mediator.Send(query);
            return Ok(progress);
        }
    }
}
