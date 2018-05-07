﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using AutoFixture.AutoFakeItEasy;
using AutoFixture;
using FakeItEasy;
using CatsListingDemo.Business;
using CatsListingDemo.RepositoryInterfaces;

namespace CatsListingDemo.Business.Tests
{
    [TestClass]
    public class PetOwnerProcessorTests
    {
        static Fixture _fixture;
        PetOwnerProcessor _systemUnderTest;
        Fake<IPetOwnerRepository> _petOwnerRepository;

        [ClassInitialize]
        public static void SetUpAutoMocking(TestContext context)
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoFakeItEasyCustomization());
        }

        [TestInitialize]
        public void SetUpSystemUnderTestAndDependencies()
        {
            // Arrange
            _petOwnerRepository = _fixture.Freeze<Fake<IPetOwnerRepository>>();
            _systemUnderTest = _fixture.Create<PetOwnerProcessor>();

        }

        [TestMethod]
        public void GetPetsByGenderShouldReturnAListOfPets()
        {
            // Act
            var result = _systemUnderTest.GetPetsByGender();

            // Assert
            result.Should().BeNullOrEmpty();
        }

    }
}
