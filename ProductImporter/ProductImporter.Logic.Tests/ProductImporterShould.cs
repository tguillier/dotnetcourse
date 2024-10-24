using AutoFixture.Xunit2;
using Moq;
using ProductImporter.Logic.Source;
using ProductImporter.Logic.Target;
using ProductImporter.Logic.Tests.Customizations;
using ProductImporter.Model;

namespace ProductImporter.Logic.Tests
{
    public class ProductImporterShould
    {
        [Theory, AutoMoqData]
        public async Task Write_One_Product_For_Each_Read_Product(
            [Frozen] Mock<IProductSource> productSourceMock,
            [Frozen] Mock<IProductTarget> productTargetMock,
            ProductImporter productImporter,
            int numberOfProduct)
        {
            // Arrange
            var productCounter = 0;

            productSourceMock
                .Setup(x => x.HasMoreProducts())
                .Callback(() => productCounter++)
                .Returns(() => productCounter <= numberOfProduct);

            // Act
            await productImporter.RunAsync();

            // Assert
            productTargetMock.Verify(x => x.AddProduct(It.IsAny<Product>()), Times.Exactly(numberOfProduct));
        }
    }
}