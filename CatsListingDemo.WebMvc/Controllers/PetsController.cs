﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatsListingDemo.BusinessInterfaces;
using CatsListingDemo.Domain;
using CatsListingDemo.WebMvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatsListingDemo.WebMvc.Controllers
{
    /// <summary>
    /// Provides server-side logic for the portions of the UI that relate to pets.
    /// </summary>
    public class PetsController : Controller
    {
        private readonly IPetOwnerService _petOwnerService;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="petOwnerService">How to process pet owners related data according to business rules.</param>
        public PetsController(IPetOwnerService petOwnerService)
        {
            _petOwnerService = petOwnerService;
        }

        // GET: Pets
        /// <summary>
        /// Renders the pets view.
        /// </summary>
        /// <returns>The view.</returns>
        public async Task<ActionResult> Cats()
        {
            var petTypeFilter = PetType.Cat;
            var viewModel = new List<PetsByOwnersGenderViewModel>();

            var ownersByGenderGroup = (await _petOwnerService.GetAllByAsync(petTypeFilter))
                .GroupBy(owner => owner.Gender);

            ownersByGenderGroup.ToList().ForEach(groupedItem =>
            {
                var viewModelItem = new PetsByOwnersGenderViewModel
                {
                    OwnerGender = groupedItem.Key,
                    Pets = groupedItem.SelectMany(owner => {

                        return owner.Pets
                        .Where(pet => pet.Type == petTypeFilter)
                        .OrderBy(pet => pet.Name);

                    })
                };

                viewModel.Add(viewModelItem);

            });

            return View("CatsByOwnersGender", viewModel);
        }
    }
}