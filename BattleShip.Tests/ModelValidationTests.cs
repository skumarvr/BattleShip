using BattleShip.ViewModels;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattleShip.Tests
{
    public class ModelValidationTests
    {
        private IList<ValidationResult> Validate<T>(T model)
        {
            var result = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);
            Validator.TryValidateObject(model, validationContext, result, true);

            return result;
        }

        [Test]
        public void Test_Validate_Model_MarkPosition()
        {
            MarkPosition validModel;
            IList<ValidationResult> result;
            int col;
            string row;

            int startChar = (int)'A';
            int boardMaxBound = 10;

            int i;
            for ( i=0; i<boardMaxBound; i++)
            {
                col = (i + 1);
                // capital alphabet letter
                row = new string((char)(i + startChar), 1);
                // validate capital alphabet letter
                validModel = new MarkPosition() { Row = row, Col =  col};

                result = Validate<MarkPosition>(validModel);
                Assert.AreEqual(0, result.Count);

                // capital small letter
                row = new string((char)(i + startChar + 32), 1);
                // validate small alphabet letter 
                validModel = new MarkPosition() { Row = row, Col = col };

                result = Validate<MarkPosition>(validModel);
                Assert.AreEqual(0, result.Count);
            }

            col = (i + 1);
            // capital alphabet letter
            row = new string((char)(i + startChar), 1);
            // validate capital alphabet letter
            validModel = new MarkPosition() { Row = row, Col = col };

            result = Validate<MarkPosition>(validModel);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(result[0].ErrorMessage, "Allow character from 'a-j' or 'A-J'");
            Assert.AreEqual(result[1].ErrorMessage, "Allow number from 1 to 10");
            

            // capital small letter
            row = new string((char)(i + startChar + 32), 1);
            // validate small alphabet letter 
            validModel = new MarkPosition() { Row = row, Col = col };

            result = Validate<MarkPosition>(validModel);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(result[0].ErrorMessage, "Allow character from 'a-j' or 'A-J'");
            Assert.AreEqual(result[1].ErrorMessage, "Allow number from 1 to 10");
        }

        [Test]
        public void Test_Validate_Model_ShipPosition()
        {
            ShipPosition validModel;
            IList<ValidationResult> result;
            int col;
            int length;
            string row;

            int startChar = (int)'A';
            int boardMaxBound = 10;

            int i;
            for (i = 0; i < boardMaxBound; i++)
            {
                col = (i + 1);
                length = (i + 1);
                // capital alphabet letter
                row = new string((char)(i + startChar), 1);
                // validate capital alphabet letter
                validModel = new ShipPosition() { Row = row, Col = col, Length = length, Vertical = (length%2==0) };

                result = Validate<ShipPosition>(validModel);
                Assert.AreEqual(0, result.Count);

                // capital small letter
                row = new string((char)(i + startChar + 32), 1);
                // validate small alphabet letter 
                validModel = new ShipPosition() { Row = row, Col = col, Length = length, Vertical = (length % 2 == 0) };

                result = Validate<ShipPosition>(validModel);
                Assert.AreEqual(0, result.Count);
            }

            col = (i + 1);
            length = (i + 1);
            // capital alphabet letter
            row = new string((char)(i + startChar), 1);
            // validate capital alphabet letter
            validModel = new ShipPosition() { Row = row, Col = col, Length = length, Vertical = (length % 2 == 0) };

            result = Validate<ShipPosition>(validModel);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(result[0].ErrorMessage, "Allow character from 'a-j' or 'A-J'");
            Assert.AreEqual(result[1].ErrorMessage, "Allow number from 1 to 10");
            Assert.AreEqual(result[2].ErrorMessage, "Allow number from 1 to 10");

            // capital small letter
            row = new string((char)(i + startChar + 32), 1);
            // validate small alphabet letter 
            validModel = new ShipPosition() { Row = row, Col = col, Length = length, Vertical = (length % 2 == 0) };

            result = Validate<ShipPosition>(validModel);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(result[0].ErrorMessage, "Allow character from 'a-j' or 'A-J'");
            Assert.AreEqual(result[1].ErrorMessage, "Allow number from 1 to 10");
            Assert.AreEqual(result[2].ErrorMessage, "Allow number from 1 to 10");
        }
    }
}
