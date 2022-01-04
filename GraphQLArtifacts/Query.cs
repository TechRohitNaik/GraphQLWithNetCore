﻿using System.Linq;
using GraphQLAPI.DBContext;
using GraphQLAPI.Models;
using HotChocolate;
using HotChocolate.Data;

namespace GraphQLAPI.GraphQLArtifacts
{
    /// <summary>
    /// Represents the queries available.
    /// </summary>
    [GraphQLDescription("Represents the queries available.")]
    public class Query
    {
        /// <summary>
        /// Gets the queryable <see cref="Maker"/>.
        /// </summary>
        /// <param name="context">The <see cref="AppDbContext"/>.</param>
        /// <returns>The queryable <see cref="Maker"/>.</returns>
        [UseDbContext(typeof(AppDbContext))]
        //[UseFiltering]
        //[UseSorting]
        [GraphQLDescription("Gets the queryable platform.")]
        public IQueryable<Maker> GetMaker([ScopedService] AppDbContext context)
        {
            return context.Makers;
        }
        
    }
}
