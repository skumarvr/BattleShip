using BattleShip.Validators;
using BattleShip.ViewModels;
using FluentValidation;
using FluentValidation.Results;
using NUnit.Framework;
using System.Collections.Generic;

namespace BattleShip.Tests
{
    public class ModelValidationTests
    {
        [Test]
        public void Test_Validate_Model_MarkPosition()
        {
            MarkPosition validModel;
            ValidationResult result;
            var validator = new MarkPositionValidator();
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

                result = validator.Validate(validModel);
                Assert.AreEqual(true, result.IsValid);

                // capital small letter
                row = new string((char)(i + startChar + 32), 1);
                // validate small alphabet letter 
                validModel = new MarkPosition() { Row = row, Col = col };

                result = validator.Validate(validModel);
                Assert.AreEqual(true, result.IsValid);
            }

            col = (i + 1);
            // capital alphabet letter
            row = new string((char)(i + startChar), 1);
            // validate capital alphabet letter
            validModel = new MarkPosition() { Row = row, Col = col };

            result = validator.Validate(validModel);
            Assert.AreEqual(false, result.IsValid);
            Assert.AreEqual(2, result.Errors.Count);
            Assert.AreEqual(result.Errors[0].ErrorMessage, "Please enter character from 'a-j' or 'A-J'");
            Assert.AreEqual(result.Errors[1].ErrorMessage, "Please enter number from 1 to 10");


            // capital small letter
            row = new string((char)(i + startChar + 32), 1);
            // validate small alphabet letter 
            validModel = new MarkPosition() { Row = row, Col = col };

            result = validator.Validate(validModel);
            Assert.AreEqual(false, result.IsValid);
            Assert.AreEqual(2, result.Errors.Count);
            Assert.AreEqual(result.Errors[0].ErrorMessage, "Please enter character from 'a-j' or 'A-J'");
            Assert.AreEqual(result.Errors[1].ErrorMessage, "Please enter number from 1 to 10");
        }

        [Test]
        public void Test_Validate_Model_ShipPosition()
        {
            ShipPosition validModel;
            ValidationResult result;
            var validator = new ShipPositionValidator();
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

                result = validator.Validate(validModel);
                Assert.AreEqual(true, result.IsValid);

                // capital small letter
                row = new string((char)(i + startChar + 32), 1);
                // validate small alphabet letter 
                validModel = new ShipPosition() { Row = row, Col = col, Length = length, Vertical = (length % 2 == 0) };

                result = validator.Validate(validModel);
                Assert.AreEqual(true, result.IsValid);
            }

            col = (i + 1);
            length = (i + 1);
            // capital alphabet letter
            row = new string((char)(i + startChar), 1);
            // validate capital alphabet letter
            validModel = new ShipPosition() { Row = row, Col = col, Length = length, Vertical = (length % 2 == 0) };

            result = validator.Validate(validModel);
            Assert.AreEqual(false, result.IsValid);
            Assert.AreEqual(3, result.Errors.Count);
            Assert.AreEqual(result.Errors[0].ErrorMessage, "Please enter character from 'a-j' or 'A-J'");
            Assert.AreEqual(result.Errors[1].ErrorMessage, "Please enter number from 1 to 10");
            Assert.AreEqual(result.Errors[2].ErrorMessage, "Please enter number from 1 to 10");

            // capital small letter
            row = new string((char)(i + startChar + 32), 1);
            // validate small alphabet letter 
            validModel = new ShipPosition() { Row = row, Col = col, Length = length, Vertical = (length % 2 == 0) };

            result = validator.Validate(validModel);
            Assert.AreEqual(false, result.IsValid);
            Assert.AreEqual(3, result.Errors.Count);
            Assert.AreEqual(result.Errors[0].ErrorMessage, "Please enter character from 'a-j' or 'A-J'");
            Assert.AreEqual(result.Errors[1].ErrorMessage, "Please enter number from 1 to 10");
            Assert.AreEqual(result.Errors[2].ErrorMessage, "Please enter number from 1 to 10");
        }
    }
}
