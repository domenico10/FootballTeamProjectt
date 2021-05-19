using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFL.Models
{
    public class TeamListViewModel : TeamViewModel
    {
        public List<Team> Teams { get; set; }
        private List<Conference> conferences;
        private List<Division> divisions;

        public List<Conference> Conferences { 
            get => conferences; //get the list
            set //set the list
            {
                conferences = value;
                conferences.Insert(0, new Conference { ConferenceID = "all", Name = "All" });// add "all" value to the conference table
            }
        }
        public List<Division> Divisions
        {
            get => divisions; 
            set
            {
                divisions = value;
                divisions.Insert(0, new Division { DivisionID = "all", Name = "All" });// add "all" value to the division table
            }
        }

        public string CheckActiveConference(string c) =>
            c.ToLower() == ActiveConf.ToLower() ? "active" : ""; // method to check if the conference property is the same passed into the method. so highlight it using 'active' CSS
        public string CheckActiveDivision(string d) =>
            d.ToLower() == ActiveDiv.ToLower() ? "active" : "";// method to check if the division property is the same passed into the method. so highlight it using 'active' CSS
    }
}
