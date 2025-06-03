using Application.CQRS.Commands;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Handlers
{
    public class PurchaseCourseCommandHandler(
        IUserCourseRepository userCourseRepository,
        IFinancialCourseRepository courseRepository)
        : IRequestHandler<PurchaseCourseCommand, string>
    {
        public async Task<string> Handle(PurchaseCourseCommand request, CancellationToken cancellationToken)
        {
            // Check if the user already purchased this course
            var existingPurchase = await userCourseRepository.GetByUserAndCourseIdAsync(request.UserId, request.CourseId);
            if (existingPurchase != null)
            {
                // If the course was previously purchased but deactivated, reactivate it
                if (!existingPurchase.IsActive)
                {
                    existingPurchase.IsActive = true;
                    await userCourseRepository.UpdateAsync(existingPurchase);
                }
                return existingPurchase.Id!;
            }

            // Get the course to determine the price
            var course = await courseRepository.GetByIdAsync(request.CourseId);
            if (course == null)
            {
                throw new Exception("Course not found");
            }

            // Calculate the actual price (considering discount)
            decimal priceToPay = course.Price;
            if (course.Discount > 0)
            {
                priceToPay -= (course.Price * course.Discount / 100);
            }

            // Create a new user course record
            var userCourse = new UserCourse
            {
                UserId = request.UserId,
                CourseId = request.CourseId,
                PurchaseDate = DateTime.UtcNow,
                PricePaid = priceToPay,
                IsActive = true
            };

            await userCourseRepository.CreateAsync(userCourse);
            return userCourse.Id!;
        }
    }
}
