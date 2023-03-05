namespace MaintainabilityIndex.Sample.Testing.Logic
{
    /// <summary>
    /// Sample type designed to inspect <see cref="Food"/> and determine the undelying <see cref="FoodType"/>.
    /// </summary>
    public class FoodClassifier
    {
        /// <summary>
        /// Gets a <see cref="FoodType"/> based on the supplied <see cref="Food"/>.
        /// Maintainability index: 40.
        /// </summary>
        /// <param name="foodToClassify">The <see cref="Food"/> to be classified.</param>
        /// <returns>A <see cref="FoodType"/> based on the supplied <see cref="Food"/>.</returns>
        public FoodType GetFoodTypeSampleOne(Food foodToClassify)
        {
            // General issue - string handling/matching
            if (foodToClassify != null)
            {
                if (!string.IsNullOrWhiteSpace(foodToClassify.InternalRef))
                {
                    // Bug one - unexpected altering of object state
                    foodToClassify.InternalRef = foodToClassify.InternalRef.ToUpper();

                    if (foodToClassify.InternalRef == "B" || foodToClassify.InternalRef == "BC")
                        return FoodType.Biscuits;
                    else if (foodToClassify.InternalRef == "D" || foodToClassify.InternalRef == "DN")
                        return FoodType.Doughnuts;
                    else if (foodToClassify.InternalRef == "S" || foodToClassify.InternalRef == "SW")
                        return FoodType.Sandwiches;
                    else if (foodToClassify.InternalRef == "V" || foodToClassify.InternalRef == "VG")
                        return FoodType.Vegetable;
                    else if (foodToClassify.InternalRef == "F" || foodToClassify.InternalRef == "FR")
                        return FoodType.Fruit;
                    else if (foodToClassify.InternalRef == "P" || foodToClassify.InternalRef == "PA")
                        return FoodType.Pasta;
                    else if (foodToClassify.InternalRef == "C" || foodToClassify.InternalRef == "CK")
                        return FoodType.Cake;
                    else if (foodToClassify.InternalRef == "M" || foodToClassify.InternalRef == "MS")
                        return FoodType.Milkshake;
                    else if (foodToClassify.InternalRef == "G" || foodToClassify.InternalRef == "GR")
                        return FoodType.Grain;
                    else if (foodToClassify.InternalRef == "R" || foodToClassify.InternalRef == "RB" || foodToClassify.InternalRef == "RBCBF")
                        return FoodType.RobocopBabyFood;
                }
            }
            else
            {
                // Bug two - null reference exception
                string externalSupplierFoodRef = foodToClassify.ExternalSupplierRef.ToUpper();

                // Bug three - pasta can be misclassified as biscuits
                if (externalSupplierFoodRef.Contains("BIS") || externalSupplierFoodRef.Contains("AF00"))
                    return FoodType.Biscuits;
                else if (externalSupplierFoodRef.Contains("DOUGH") || externalSupplierFoodRef.Contains("AF01"))
                    return FoodType.Doughnuts;
                else if (externalSupplierFoodRef.Contains("SAND") || externalSupplierFoodRef.Contains("AF02"))
                    return FoodType.Sandwiches;
                else if (externalSupplierFoodRef.Contains("VEGE") || externalSupplierFoodRef.Contains("AF03"))
                    return FoodType.Vegetable;
                else if (externalSupplierFoodRef.Contains("FRU") || externalSupplierFoodRef.Contains("AF04"))
                    return FoodType.Fruit;
                else if (externalSupplierFoodRef.Contains("PAS") || externalSupplierFoodRef.Contains("AF001"))
                    return FoodType.Pasta;
                else if (externalSupplierFoodRef.Contains("CAK") || externalSupplierFoodRef.Contains("CAKE01") || externalSupplierFoodRef.Contains("AF05"))
                    return FoodType.Cake;
                else if (externalSupplierFoodRef.Contains("MIL") || externalSupplierFoodRef.Contains("MISHK") || externalSupplierFoodRef.Contains("AF06"))
                    return FoodType.Milkshake;
                else if (externalSupplierFoodRef.Contains("GRA") || externalSupplierFoodRef.Contains("AF07"))
                    return FoodType.Grain;
                else if (externalSupplierFoodRef.Contains("ROBO") || externalSupplierFoodRef.Contains("ROBCOP") || externalSupplierFoodRef.Contains("RBCPBF") || externalSupplierFoodRef.Contains("AF08"))
                    return FoodType.RobocopBabyFood;
                else if (externalSupplierFoodRef.Contains("ACB") || externalSupplierFoodRef.Contains("AF09"))
                    return FoodType.AlienCremeBrulee;
                else if (externalSupplierFoodRef.Contains("PUSHP") || externalSupplierFoodRef.Contains("AF10"))
                    return FoodType.PushPop;
            }

            return FoodType.Unknown;
        }

        /// <summary>
        /// Gets a <see cref="FoodType"/> based on the supplied <see cref="Food"/>.
        /// Maintainability index: 39. It's worse!
        /// </summary>
        /// <param name="foodToClassify">The <see cref="Food"/> to be classified.</param>
        /// <returns>A <see cref="FoodType"/> based on the supplied <see cref="Food"/>.</returns>
        public FoodType GetFoodTypeSampleTwo(Food foodToClassify)
        {
            if (foodToClassify == null)
            {
                return FoodType.Unknown;
            }

            // General issue - string handling/matching
            if (!string.IsNullOrWhiteSpace(foodToClassify.InternalRef))
            {
                // Bug one - unexpected altering of object state
                foodToClassify.InternalRef = foodToClassify.InternalRef.ToUpper();

                if (foodToClassify.InternalRef == "B" || foodToClassify.InternalRef == "BC")
                    return FoodType.Biscuits;
                else if (foodToClassify.InternalRef == "D" || foodToClassify.InternalRef == "DN")
                    return FoodType.Doughnuts;
                else if (foodToClassify.InternalRef == "S" || foodToClassify.InternalRef == "SW")
                    return FoodType.Sandwiches;
                else if (foodToClassify.InternalRef == "V" || foodToClassify.InternalRef == "VG")
                    return FoodType.Vegetable;
                else if (foodToClassify.InternalRef == "F" || foodToClassify.InternalRef == "FR")
                    return FoodType.Fruit;
                else if (foodToClassify.InternalRef == "P" || foodToClassify.InternalRef == "PA")
                    return FoodType.Pasta;
                else if (foodToClassify.InternalRef == "C" || foodToClassify.InternalRef == "CK")
                    return FoodType.Cake;
                else if (foodToClassify.InternalRef == "M" || foodToClassify.InternalRef == "MS")
                    return FoodType.Milkshake;
                else if (foodToClassify.InternalRef == "G" || foodToClassify.InternalRef == "GR")
                    return FoodType.Grain;
                else if (foodToClassify.InternalRef == "R" || foodToClassify.InternalRef == "RB" || foodToClassify.InternalRef == "RBCBF")
                    return FoodType.RobocopBabyFood;
            }
            else
            {
                // Bug two - null reference exception
                string externalSupplierFoodRef = foodToClassify.ExternalSupplierRef.ToUpper();

                // Bug three - pasta can be misclassified as biscuits
                if (externalSupplierFoodRef.Contains("BIS") || externalSupplierFoodRef.Contains("AF00"))
                    return FoodType.Biscuits;
                else if (externalSupplierFoodRef.Contains("DOUGH") || externalSupplierFoodRef.Contains("AF01"))
                    return FoodType.Doughnuts;
                else if (externalSupplierFoodRef.Contains("SAND") || externalSupplierFoodRef.Contains("AF02"))
                    return FoodType.Sandwiches;
                else if (externalSupplierFoodRef.Contains("VEGE") || externalSupplierFoodRef.Contains("AF03"))
                    return FoodType.Vegetable;
                else if (externalSupplierFoodRef.Contains("FRU") || externalSupplierFoodRef.Contains("AF04"))
                    return FoodType.Fruit;
                else if (externalSupplierFoodRef.Contains("PAS") || externalSupplierFoodRef.Contains("AF001"))
                    return FoodType.Pasta;
                else if (externalSupplierFoodRef.Contains("CAK") || externalSupplierFoodRef.Contains("CAKE01") || externalSupplierFoodRef.Contains("AF05"))
                    return FoodType.Cake;
                else if (externalSupplierFoodRef.Contains("MIL") || externalSupplierFoodRef.Contains("MISHK") || externalSupplierFoodRef.Contains("AF06"))
                    return FoodType.Milkshake;
                else if (externalSupplierFoodRef.Contains("GRA") || externalSupplierFoodRef.Contains("AF07"))
                    return FoodType.Grain;
                else if (externalSupplierFoodRef.Contains("ROBO") || externalSupplierFoodRef.Contains("ROBCOP") || externalSupplierFoodRef.Contains("RBCPBF") || externalSupplierFoodRef.Contains("AF08"))
                    return FoodType.RobocopBabyFood;
                else if (externalSupplierFoodRef.Contains("ACB") || externalSupplierFoodRef.Contains("AF09"))
                    return FoodType.AlienCremeBrulee;
                else if (externalSupplierFoodRef.Contains("PUSHP") || externalSupplierFoodRef.Contains("AF10"))
                    return FoodType.PushPop;
            }

            return FoodType.Unknown;
        }
    }
}
