using MaintainabilityIndex.Sample.Testing.Model;

namespace MaintainabilityIndex.Sample.Testing.Logic
{
    /// <summary>
    /// Sample type designed to inspect <see cref="Food"/> and determine the undelying <see cref="FoodType"/>.
    /// </summary>
    public class FoodClassifier
    {
        /// <summary>
        /// Gets a <see cref="FoodType"/> based on the supplied <see cref="Food"/>.
        /// Maintainability index: 39.
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
                else if (externalSupplierFoodRef.Contains("PAS") || externalSupplierFoodRef.Contains("AF010"))
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
        /// Maintainability index: 38. It's worse!
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
                else if (externalSupplierFoodRef.Contains("PAS") || externalSupplierFoodRef.Contains("AF010"))
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
        /// Maintainability index: 38.
        /// </summary>
        /// <param name="foodToClassify">The <see cref="Food"/> to be classified.</param>
        /// <returns>A <see cref="FoodType"/> based on the supplied <see cref="Food"/>.</returns>
        public FoodType GetFoodTypeSampleThree(Food foodToClassify)
        {
            if (foodToClassify == null
                || (string.IsNullOrWhiteSpace(foodToClassify.InternalRef) && string.IsNullOrWhiteSpace(foodToClassify.ExternalSupplierRef)))
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
                else if (externalSupplierFoodRef.Contains("PAS") || externalSupplierFoodRef.Contains("AF010"))
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
        /// Maintainability index: 46.
        /// </summary>
        /// <param name="foodToClassify">The <see cref="Food"/> to be classified.</param>
        /// <returns>A <see cref="FoodType"/> based on the supplied <see cref="Food"/>.</returns>
        public FoodType GetFoodTypeSampleFour(Food foodToClassify)
        {
            string? foodReference = ((!string.IsNullOrWhiteSpace(foodToClassify?.InternalRef) 
                ? foodToClassify?.InternalRef 
                : foodToClassify?.ExternalSupplierRef) ?? string.Empty)
                .ToUpper();

            if (foodReference == "B" || foodReference == "BC" || foodReference.Contains("BIS") || foodReference.Contains("AF00"))
                return FoodType.Biscuits;
            else if (foodReference == "D" || foodReference == "DN" || foodReference.Contains("DOUGH") || foodReference.Contains("AF01"))
                return FoodType.Doughnuts;
            else if (foodReference == "S" || foodReference == "SW" || foodReference.Contains("SAND") || foodReference.Contains("AF02"))
                return FoodType.Sandwiches;
            else if (foodReference == "V" || foodReference == "VG" || foodReference.Contains("VEGE") || foodReference.Contains("AF03"))
                return FoodType.Vegetable;
            else if (foodReference == "F" || foodReference == "FR" || foodReference.Contains("FRU") || foodReference.Contains("AF04"))
                return FoodType.Fruit;
            else if (foodReference == "P" || foodReference == "PA" || foodReference.Contains("PAS") || foodReference.Contains("AF010"))
                return FoodType.Pasta;
            else if (foodReference == "C" || foodReference == "CK" || foodReference.Contains("CAK") || foodReference.Contains("CAKE01") || foodReference.Contains("AF05"))
                return FoodType.Cake;
            else if (foodReference == "M" || foodReference == "MS" || foodReference.Contains("MIL") || foodReference.Contains("MISHK") || foodReference.Contains("AF06"))
                return FoodType.Milkshake;
            else if (foodReference == "G" || foodReference == "GR" || foodReference.Contains("GRA") || foodReference.Contains("AF07"))
                return FoodType.Grain;
            else if (foodReference == "R" || foodReference == "RB" || foodReference == "RBCBF" || foodReference.Contains("ROBO") || foodReference.Contains("ROBCOP") || foodReference.Contains("RBCPBF") || foodReference.Contains("AF08"))
                return FoodType.RobocopBabyFood;
            else if (foodReference.Contains("ACB") || foodReference.Contains("AF09"))
                return FoodType.AlienCremeBrulee;
            else if (foodReference.Contains("PUSHP") || foodReference.Contains("AF10"))
                return FoodType.PushPop;
            else
                return FoodType.Unknown;
        }

        /// <summary>
        /// Gets a <see cref="FoodType"/> based on the supplied <see cref="Food"/>.
        /// Maintainability index: 39. Braces dropped our score.
        /// </summary>
        /// <param name="foodToClassify">The <see cref="Food"/> to be classified.</param>
        /// <returns>A <see cref="FoodType"/> based on the supplied <see cref="Food"/>.</returns>
        public FoodType GetFoodTypeSampleFive(Food foodToClassify)
        {
            string? foodReference = ((!string.IsNullOrWhiteSpace(foodToClassify?.InternalRef)
                ? foodToClassify?.InternalRef
                : foodToClassify?.ExternalSupplierRef) ?? string.Empty)
                .ToUpper();

            if (foodReference == "B" || foodReference == "BC" || foodReference.Contains("BIS") || foodReference.Contains("AF00"))
            {
                return FoodType.Biscuits;
            }
            else if (foodReference == "D" || foodReference == "DN" || foodReference.Contains("DOUGH") || foodReference.Contains("AF01"))
            {
                return FoodType.Doughnuts;
            }
            else if (foodReference == "S" || foodReference == "SW" || foodReference.Contains("SAND") || foodReference.Contains("AF02"))
            {
                return FoodType.Sandwiches;
            }
            else if (foodReference == "V" || foodReference == "VG" || foodReference.Contains("VEGE") || foodReference.Contains("AF03"))
            {
                return FoodType.Vegetable;
            }
            else if (foodReference == "F" || foodReference == "FR" || foodReference.Contains("FRU") || foodReference.Contains("AF04"))
            {
                return FoodType.Fruit;
            }
            else if (foodReference == "P" || foodReference == "PA" || foodReference.Contains("PAS") || foodReference.Contains("AF010"))
            {
                return FoodType.Pasta;
            }
            else if (foodReference == "C" || foodReference == "CK" || foodReference.Contains("CAK") || foodReference.Contains("CAKE01") || foodReference.Contains("AF05"))
            {
                return FoodType.Cake;
            }
            else if (foodReference == "M" || foodReference == "MS" || foodReference.Contains("MIL") || foodReference.Contains("MISHK") || foodReference.Contains("AF06"))
            {
                return FoodType.Milkshake;
            }
            else if (foodReference == "G" || foodReference == "GR" || foodReference.Contains("GRA") || foodReference.Contains("AF07"))
            {
                return FoodType.Grain;
            }
            else if (foodReference == "R" || foodReference == "RB" || foodReference == "RBCBF" || foodReference.Contains("ROBO") || foodReference.Contains("ROBCOP") || foodReference.Contains("RBCPBF") || foodReference.Contains("AF08"))
            {
                return FoodType.RobocopBabyFood;
            }
            else if (foodReference.Contains("ACB") || foodReference.Contains("AF09"))
            {
                return FoodType.AlienCremeBrulee;
            }
            else if (foodReference.Contains("PUSHP") || foodReference.Contains("AF10"))
            {
                return FoodType.PushPop;
            }
            else
            {
                return FoodType.Unknown;
            }
        }

        /// <summary>
        /// Gets a <see cref="FoodType"/> based on the supplied <see cref="Food"/>.
        /// Maintainability index: 65.
        /// </summary>
        /// <param name="foodToClassify">The <see cref="Food"/> to be classified.</param>
        /// <returns>A <see cref="FoodType"/> based on the supplied <see cref="Food"/>.</returns>
        public FoodType GetFoodTypeSampleSix(Food foodToClassify)
        {
            string? foodReference = ((!string.IsNullOrWhiteSpace(foodToClassify?.InternalRef)
                ? foodToClassify?.InternalRef
                : foodToClassify?.ExternalSupplierRef) ?? string.Empty)
                .ToUpper();

            return foodReference switch
            {
                string reference when reference == "B" || reference == "BC" || reference.Contains("BIS") || reference.Contains("AF00") => FoodType.Biscuits,
                string reference when reference == "D" || reference == "DN" || reference.Contains("DOUGH") || reference.Contains("AF01") => FoodType.Doughnuts,
                string reference when reference == "S" || reference == "SW" || reference.Contains("SAND") || reference.Contains("AF02") => FoodType.Sandwiches,
                string reference when reference == "V" || reference == "VG" || reference.Contains("VEGE") || reference.Contains("AF03") => FoodType.Vegetable,
                string reference when reference == "F" || reference == "FR" || reference.Contains("FRU") || reference.Contains("AF04") => FoodType.Fruit,
                string reference when reference == "P" || reference == "PA" || reference.Contains("PAS") || reference.Contains("AF010") => FoodType.Pasta,
                string reference when reference == "C" || reference == "CK" || reference.Contains("CAK") || reference.Contains("CAKE01") || reference.Contains("AF05") => FoodType.Cake,
                string reference when reference == "M" || reference == "MS" || reference.Contains("MIL") || reference.Contains("MISHK") || reference.Contains("AF06") => FoodType.Milkshake,
                string reference when reference == "G" || reference == "GR" || reference.Contains("GRA") || reference.Contains("AF07") => FoodType.Grain,
                string reference when reference == "R" || reference == "RB" || reference == "RBCBF" || reference.Contains("ROBO") || reference.Contains("ROBCOP") || reference.Contains("RBCPBF") || reference.Contains("AF08") => FoodType.RobocopBabyFood,
                string reference when reference.Contains("ACB") || reference.Contains("AF09") => FoodType.AlienCremeBrulee,
                string reference when reference.Contains("PUSHP") || reference.Contains("AF10") => FoodType.PushPop,
                _ => FoodType.Unknown
            };
        }

        /// <summary>
        /// Gets a <see cref="FoodType"/> based on the supplied <see cref="Food"/>.
        /// Does cheating with abstractions affect the score?
        /// Maintainability index: 85.
        /// </summary>
        /// <param name="foodToClassify">The <see cref="Food"/> to be classified.</param>
        /// <returns>A <see cref="FoodType"/> based on the supplied <see cref="Food"/>.</returns>
        public FoodType GetFoodTypeSampleSeven(Food foodToClassify)
        {
            string foodReference = GetFoodReferenceFromFood(foodToClassify);
            
            return ExtractFoodTypeFromFood(foodReference);
        }

        #region Abstractions for Sample Seven

        private static FoodType ExtractFoodTypeFromFood(string foodReference) =>
            foodReference switch
            {
                string reference when reference == "B" || reference == "BC" || reference.Contains("BIS") || reference.Contains("AF00") => FoodType.Biscuits,
                string reference when reference == "D" || reference == "DN" || reference.Contains("DOUGH") || reference.Contains("AF01") => FoodType.Doughnuts,
                string reference when reference == "S" || reference == "SW" || reference.Contains("SAND") || reference.Contains("AF02") => FoodType.Sandwiches,
                string reference when reference == "V" || reference == "VG" || reference.Contains("VEGE") || reference.Contains("AF03") => FoodType.Vegetable,
                string reference when reference == "F" || reference == "FR" || reference.Contains("FRU") || reference.Contains("AF04") => FoodType.Fruit,
                string reference when reference == "P" || reference == "PA" || reference.Contains("PAS") || reference.Contains("AF010") => FoodType.Pasta,
                string reference when reference == "C" || reference == "CK" || reference.Contains("CAK") || reference.Contains("CAKE01") || reference.Contains("AF05") => FoodType.Cake,
                string reference when reference == "M" || reference == "MS" || reference.Contains("MIL") || reference.Contains("MISHK") || reference.Contains("AF06") => FoodType.Milkshake,
                string reference when reference == "G" || reference == "GR" || reference.Contains("GRA") || reference.Contains("AF07") => FoodType.Grain,
                string reference when reference == "R" || reference == "RB" || reference == "RBCBF" || reference.Contains("ROBO") || reference.Contains("ROBCOP") || reference.Contains("RBCPBF") || reference.Contains("AF08") => FoodType.RobocopBabyFood,
                string reference when reference.Contains("ACB") || reference.Contains("AF09") => FoodType.AlienCremeBrulee,
                string reference when reference.Contains("PUSHP") || reference.Contains("AF10") => FoodType.PushPop,
                _ => FoodType.Unknown
            };

        private static string GetFoodReferenceFromFood(Food foodToClassify) =>
            ((!string.IsNullOrWhiteSpace(foodToClassify?.InternalRef)
                ? foodToClassify?.InternalRef
                : foodToClassify?.ExternalSupplierRef) ?? string.Empty)
                .ToUpper();

        #endregion Abstractions for Sample Seven
    }
}
