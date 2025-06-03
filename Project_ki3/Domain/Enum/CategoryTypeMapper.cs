using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Enum
{
    public static class CategoryTypeMapper
    {
        private static readonly Dictionary<Category, Type> CategoryToTypeMap = new()
        {
            // Mapping DebtLoan categories
            { Category.Mortgage, Type.DebtLoan },
            { Category.CreditCard, Type.DebtLoan },
            { Category.PersonalLoan, Type.DebtLoan },
            { Category.PayInterest, Type.DebtLoan },

            // Mapping Expense categories
            { Category.FoodAndBeverage, Type.Expense },
            { Category.BillsAndUtilities, Type.Expense },
            { Category.Rentals, Type.Expense },
            { Category.WaterBill, Type.Expense },
            { Category.PhoneBill, Type.Expense },
            { Category.ElectricityBill, Type.Expense },
            { Category.GasBill, Type.Expense },
            { Category.TelevisionBill, Type.Expense },
            { Category.InternetBill, Type.Expense },
            { Category.OtherUtilityBills, Type.Expense },
            { Category.Shopping, Type.Expense },
            { Category.PersonalItems, Type.Expense },
            { Category.Houseware, Type.Expense },
            { Category.Makeup, Type.Expense },
            { Category.Family, Type.Expense },
            { Category.HomeMaintenance, Type.Expense },
            { Category.Pets, Type.Expense },
            { Category.Transportation, Type.Expense },
            { Category.VehicleMaintenance, Type.Expense },
            { Category.HealthAndFitness, Type.Expense },
            { Category.MedicalCheckUp, Type.Expense },
            { Category.Fitness, Type.Expense },
            { Category.Education, Type.Expense },
            { Category.Entertainment, Type.Expense },
            { Category.StreamingService, Type.Expense },
            { Category.FunMoney, Type.Expense },
            { Category.GiftAndDonations, Type.Expense },
            { Category.Insurances, Type.Expense },
            { Category.Investment, Type.Expense },
            { Category.OtherExpense, Type.Expense },
            { Category.OutgoingTransfer, Type.Expense },
            { Category.UncategorizedExpense, Type.Expense },

            // Mapping Income categories
            { Category.Salary, Type.Income },
            { Category.OtherIncome, Type.Income },
            { Category.IncomingTransfer, Type.Income },
            { Category.CollectInterest, Type.Income },
            { Category.UncategorizedIncome, Type.Income }
        };

        public static Type GetTypeForCategory(Category category)
        {
            if (CategoryToTypeMap.TryGetValue(category, out var type))
            {
                return type;
            }

            throw new ArgumentException($"No corresponding Type found for Category: {category}");
        }

        public static IEnumerable<Category> GetCategoriesForType(Type type)
        {
            return CategoryToTypeMap
                .Where(pair => pair.Value == type)
                .Select(pair => pair.Key);
        }
    }
}
