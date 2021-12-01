﻿namespace CourseWork.Core.Models.EmailTemplate
{
    using System.Collections.Generic;
    using Database.Entities.Identity;

    /// <summary>
    /// Weekly Summary Model.
    /// </summary>
    public class WeeklySummaryModel
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public AppUser User { get; set; }

        /// <summary>
        /// Gets or sets the board thread with replies models.
        /// </summary>
        /// <value>
        /// The board thread with replies models.
        /// </value>
        public List<BoardThreadWithRepliesModel> BoardThreadWithRepliesModels { get; set; }
    }
}