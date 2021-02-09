using Number.Core;
using Number.Db;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HomeWorkTrial.Tests
{
    [TestFixture]
    public class IinputServicesTests
    {
        private Input _input;
        private InputServices _inputServices;
        [SetUp]
        public void SetUp()
        {
            _input = new Input();
            _inputServices = new InputServices();
        }

        [Test]
        public void InputServices_GetFobanacciCachingIsOnTimeSpentIsSmallerThanWithoutCaching_ReturnTimeWithCachingIsSmallerThanWithout()
        {
            var process = Process.GetCurrentProcess();
            _input.FirstIndex = 1;
            _input.LastIndex = 9;
            _input.isCached = false;
            _inputServices.GetFobanacci(_input);
            long memoryUsedWithoutCaching = process.PrivateMemorySize64;

            var secondProcess = Process.GetCurrentProcess();
            _input.FirstIndex = 1;
            _input.LastIndex = 9;
            _input.isCached = true;
            _inputServices.GetFobanacci(_input);
            long memoryUsedWithCaching = secondProcess.PrivateMemorySize64;

            Assert.That(memoryUsedWithoutCaching, Is.LessThan(memoryUsedWithCaching));

        }

        [Test]
        [TestCase(1,10,0.001)]
        public void InputServices_GetFobanacciFirstNumberGeneration_ReturnEmptyListIfTimeExceedsInputFromUser(int first,int last,double time)
        {
            _input.FirstIndex = first;
            _input.LastIndex = last;
            _input.Time = TimeSpan.FromMilliseconds(time);
            var result = _inputServices.GetFobanacci(_input);
            Assert.That(result, Is.EqualTo(new List<int> { }));
        }
        [Test]
        public void InputServices_GetFobanacciWithCorrectInputs_PassIfListOfGeneratedValuesMathcesExpectedBehavior()
        {
            _input.FirstIndex = 1;
            _input.LastIndex = 6;
            var result = _inputServices.GetFobanacci(_input);
            Assert.That(result, Is.EqualTo(new List<int> { 1, 1, 2, 3, 5, 8 }));

        }
        [Test]
        public void InputServices_GetFobanacciWithoutInputs_ReturnListWithZeroInteger()
        {
            var result = _inputServices.GetFobanacci(_input);
            Assert.That(result, Is.EqualTo(new List<int> { 0 }));
        }

        [Test]
        public void InputServices_InputValidationWithCorrectInputs_ReturnEmptyList()
        {
            var result = _inputServices.InputValidation(1, 6);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void InputServices_InputValidationWithSecondInputSmallerThanFirst_ReturnListWithString()
        {
            var result = _inputServices.InputValidation(5, 2);
            Assert.That(result, Is.EqualTo(new List<string> { "Second Index should be bigger than 1 to provide range of FibonachiNumbers" }));
        }

    }
}
