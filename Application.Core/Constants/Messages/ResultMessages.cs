using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Core.Constants.Messages
{
    public static class ResultMessages
    {
        public static readonly string Added = "Successfully added.";
        public static readonly string Removed = "Successfully removed.";
        public static readonly string NotBeRemoved = "Data could not be removed.";
        public static readonly string NotBeAdded = "Data could not be added.";
        public static readonly string NotBeUpdated = "Data could not be updated.";
        public static readonly string Updated = "Successfully updated.";
        public static readonly string NotFound = "Not found.";
        public static readonly string EmptyOrNullContent = "Content is empty or null";
    }
}
