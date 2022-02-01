using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Models
{
    /// <summary>
    /// Delete confirmation model
    /// </summary>
    public class GridActionConfirmationModel 
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Controller name
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// Action name
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// Window ID
        /// </summary>
        public string WindowId { get; set; }

        public GridAction GridAction { get; set; }
    }
}
