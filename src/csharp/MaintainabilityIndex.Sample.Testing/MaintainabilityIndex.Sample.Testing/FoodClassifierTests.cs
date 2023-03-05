using FluentAssertions;

namespace MaintainabilityIndex.Sample.Testing
{
    public class FoodClassifierTests
    {
        private readonly FoodClassifier foodClassifier;

        public FoodClassifierTests()
        {
            foodClassifier = new FoodClassifier();
        }

        public static IEnumerable<object[]> GetFoodTypeSampleData()
        {
            foreach (var foodWithInternalRefItem in GetFoodsWithInternalRefTestData())
            {
                yield return foodWithInternalRefItem;
            }
        }

        [Theory]
        [MemberData(nameof(GetFoodTypeSampleData))]
        public void GetFoodTypeSampleOne_Data_Test(Food foodUnderTest, FoodType expectedFoodType) =>
            foodClassifier.GetFoodTypeSampleOne(foodUnderTest)
                .Should()
                .Be(expectedFoodType);

        [Theory]
        [MemberData(nameof(GetFoodTypeSampleData))]
        public void GetFoodTypeSampleTwo_Data_Test(Food foodUnderTest, FoodType expectedFoodType) =>
            foodClassifier.GetFoodTypeSampleOne(foodUnderTest)
                .Should()
                .Be(expectedFoodType);

        private static IEnumerable<object[]> GetFoodsWithInternalRefTestData()
        {
            yield return new object[] { new Food { InternalRef = "b" }, FoodType.Biscuits };
            yield return new object[] { new Food { InternalRef = "bc" }, FoodType.Biscuits };
            yield return new object[] { new Food { InternalRef = "d" }, FoodType.Doughnuts };
            yield return new object[] { new Food { InternalRef = "dn" }, FoodType.Doughnuts };
            yield return new object[] { new Food { InternalRef = "s" }, FoodType.Sandwiches };
            yield return new object[] { new Food { InternalRef = "sw" }, FoodType.Sandwiches };
            yield return new object[] { new Food { InternalRef = "v" }, FoodType.Vegetable };
            yield return new object[] { new Food { InternalRef = "vg" }, FoodType.Vegetable };
            yield return new object[] { new Food { InternalRef = "f" }, FoodType.Fruit };
            yield return new object[] { new Food { InternalRef = "fr" }, FoodType.Fruit };
            yield return new object[] { new Food { InternalRef = "c" }, FoodType.Cake };
            yield return new object[] { new Food { InternalRef = "ck" }, FoodType.Cake };
            yield return new object[] { new Food { InternalRef = "m" }, FoodType.Milkshake };
            yield return new object[] { new Food { InternalRef = "ms" }, FoodType.Milkshake };
            yield return new object[] { new Food { InternalRef = "g" }, FoodType.Grain };
            yield return new object[] { new Food { InternalRef = "gr" }, FoodType.Grain };
            yield return new object[] { new Food { InternalRef = "r" }, FoodType.RobocopBabyFood };
            yield return new object[] { new Food { InternalRef = "rb" }, FoodType.RobocopBabyFood };
            yield return new object[] { new Food { InternalRef = "rbcbf" }, FoodType.RobocopBabyFood };
        }
    }
}
