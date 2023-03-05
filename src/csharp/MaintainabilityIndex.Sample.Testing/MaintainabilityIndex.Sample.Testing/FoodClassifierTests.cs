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

            foreach (var foodWithExternalSupplierRefItem in GetFoodsWithExternalSupplierRefData())
            {
                yield return foodWithExternalSupplierRefItem;
            }

            // Nulls (for properties and at object level - should return unknown)
            yield return new object[] { new Food { InternalRef = null }, FoodType.Unknown };
            yield return new object[] { new Food { ExternalSupplierRef = null }, FoodType.Unknown };
            yield return new object[] { null, FoodType.Unknown };
        }

        // Will result in failures
        [Theory]
        [MemberData(nameof(GetFoodTypeSampleData))]
        public void GetFoodTypeSampleOne_Data_Test(Food foodUnderTest, FoodType expectedFoodType) =>
            foodClassifier.GetFoodTypeSampleOne(foodUnderTest)
                .Should()
                .Be(expectedFoodType);

        // Will result in 'some' failures
        [Theory]
        [MemberData(nameof(GetFoodTypeSampleData))]
        public void GetFoodTypeSampleTwo_Data_Test(Food foodUnderTest, FoodType expectedFoodType) =>
            foodClassifier.GetFoodTypeSampleTwo(foodUnderTest)
                .Should()
                .Be(expectedFoodType);

        // Null reference exceptions fixed, still sub-optimal
        [Theory]
        [MemberData(nameof(GetFoodTypeSampleData))]
        public void GetFoodTypeSampleThree_Data_Test(Food foodUnderTest, FoodType expectedFoodType) =>
            foodClassifier.GetFoodTypeSampleThree(foodUnderTest)
                .Should()
                .Be(expectedFoodType);

        [Theory]
        [MemberData(nameof(GetFoodTypeSampleData))]
        public void GetFoodTypeSampleFour_Data_Test(Food foodUnderTest, FoodType expectedFoodType) =>
            foodClassifier.GetFoodTypeSampleFour(foodUnderTest)
                .Should()
                .Be(expectedFoodType);

        [Theory]
        [MemberData(nameof(GetFoodTypeSampleData))]
        public void GetFoodTypeSampleFive_Data_Test(Food foodUnderTest, FoodType expectedFoodType) =>
            foodClassifier.GetFoodTypeSampleFive(foodUnderTest)
                .Should()
                .Be(expectedFoodType);

        [Theory]
        [MemberData(nameof(GetFoodTypeSampleData))]
        public void GetFoodTypeSampleSix_Data_Test(Food foodUnderTest, FoodType expectedFoodType) =>
            foodClassifier.GetFoodTypeSampleSix(foodUnderTest)
                .Should()
                .Be(expectedFoodType);

        private static IEnumerable<object[]> GetFoodsWithExternalSupplierRefData()
        {
            yield return new object[] { new Food { ExternalSupplierRef = "bis" }, FoodType.Biscuits };
            yield return new object[] { new Food { ExternalSupplierRef = "af00" }, FoodType.Biscuits };
            yield return new object[] { new Food { ExternalSupplierRef = "dough" }, FoodType.Doughnuts };
            yield return new object[] { new Food { ExternalSupplierRef = "af01" }, FoodType.Doughnuts };
            yield return new object[] { new Food { ExternalSupplierRef = "sand" }, FoodType.Sandwiches };
            yield return new object[] { new Food { ExternalSupplierRef = "af02" }, FoodType.Sandwiches };
            yield return new object[] { new Food { ExternalSupplierRef = "vege" }, FoodType.Vegetable };
            yield return new object[] { new Food { ExternalSupplierRef = "af03" }, FoodType.Vegetable };
            yield return new object[] { new Food { ExternalSupplierRef = "fru" }, FoodType.Fruit };
            yield return new object[] { new Food { ExternalSupplierRef = "af04" }, FoodType.Fruit };
            yield return new object[] { new Food { ExternalSupplierRef = "pas" }, FoodType.Pasta };
            yield return new object[] { new Food { ExternalSupplierRef = "af010" }, FoodType.Pasta };
            yield return new object[] { new Food { ExternalSupplierRef = "cak" }, FoodType.Cake };
            yield return new object[] { new Food { ExternalSupplierRef = "cake01" }, FoodType.Cake };
            yield return new object[] { new Food { ExternalSupplierRef = "af05" }, FoodType.Cake };
            yield return new object[] { new Food { ExternalSupplierRef = "mil" }, FoodType.Milkshake };
            yield return new object[] { new Food { ExternalSupplierRef = "mishk" }, FoodType.Milkshake };
            yield return new object[] { new Food { ExternalSupplierRef = "af06" }, FoodType.Milkshake };
            yield return new object[] { new Food { ExternalSupplierRef = "gra" }, FoodType.Grain };
            yield return new object[] { new Food { ExternalSupplierRef = "af07" }, FoodType.Grain };
            yield return new object[] { new Food { ExternalSupplierRef = "robo" }, FoodType.RobocopBabyFood };
            yield return new object[] { new Food { ExternalSupplierRef = "robcop" }, FoodType.RobocopBabyFood };
            yield return new object[] { new Food { ExternalSupplierRef = "rbcpbf" }, FoodType.RobocopBabyFood };
            yield return new object[] { new Food { ExternalSupplierRef = "af08" }, FoodType.RobocopBabyFood };
            yield return new object[] { new Food { ExternalSupplierRef = "acb" }, FoodType.AlienCremeBrulee };
            yield return new object[] { new Food { ExternalSupplierRef = "af09" }, FoodType.AlienCremeBrulee };
            yield return new object[] { new Food { ExternalSupplierRef = "pushp" }, FoodType.PushPop };
            yield return new object[] { new Food { ExternalSupplierRef = "af10" }, FoodType.PushPop };
        }

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
            yield return new object[] { new Food { InternalRef = "p" }, FoodType.Pasta };
            yield return new object[] { new Food { InternalRef = "pa" }, FoodType.Pasta };
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
