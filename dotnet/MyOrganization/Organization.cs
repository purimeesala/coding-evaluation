using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;
        private List<Position> rootRresult;
        private List<Position> positions;

        public Organization()
        {
            root = CreateOrganization();
            rootRresult = new List<Position>();
            positions = new List<Position>() { new Position(root.GetTitle()) };

        }

        protected abstract Position CreateOrganization();

        /**
         * hire the given person as an employee in the position that has that title
         * 
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title
         */
        public Position addNewPositionEntitiesFromOldEntity(Position positionInput, Position positionOutput, Name personName, string title)
        {
            var employee = new Employee(personName);

            if (positionOutput.IsFilled() == false && positionInput.GetTitle() == title)
            {
                positionOutput.SetEmployee(employee);

            }
            else
            {
                positionOutput.GetEmployee();
            }
            if (positionInput.GetDirectReports().Any())
            {

                foreach (var pi in positionInput.GetDirectReports())
                {

                    var po = addNewPositionEntitiesFromOldEntity(pi, positionOutput, personName, title);
                    positionOutput.AddDirectReport(po);
                }




            }


            return positionOutput;
        }
        public Position? Hire(Name person, string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return null;
            }
            else
            {
                var employee = new Employee(person);
                //var inputPosition = new Position(title, employee);
                //var serializedRootPositions=JsonConvert.SerializeObject(root);
                //var rootPosition = ReturnPosition(new List<Position>() { root }.ToArray(), title);
                var resultRoot = new Position(root.GetTitle());

                // resultRoot.SetEmployee(employee);
                var positionResult = addNewPositionEntitiesFromOldEntity(root, resultRoot, person, root.GetTitle());


                //    if (rootPosition.IsFilled() == false)
                //    {
                //        rootPosition.SetEmployee(employee);

                //    }
                //    else
                //    {
                //        rootPosition.GetEmployee();
                //    }
                //root = rootPosition;
                //rootPosition.ToString
                // PrintOrganization(rootPosition, rootPosition.GetDirectReports().ToString());
                return positionResult;

            }

        }

        public override string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "  "));
            }
            return sb.ToString();
        }
        //private Position ExtractPositions(Position pos,List<Position> positions)
        //{
        //    foreach (Position p in pos.GetDirectReports())
        //    {
        //        positions.Add(ExtractPositions(p, positions));
        //    }
        //    return pos;
        //}

        //private Position ReturnPosition(Position[] pos, string title)
        //{
        //    var isMatchPostionFound = false;
        //    if (pos.Any(x => x.GetTitle().Contains(title)))
        //    {
        //        isMatchPostionFound = true;
        //        return pos.Single(x => x.GetTitle().Contains(title));
        //    }
        //    foreach (var item in pos)
        //    {
        //       if(isMatchPostionFound) { break; }
        //        if (item.GetDirectReports().ToArray().Any())
        //        {
        //            return ReturnPosition(item.GetDirectReports().ToArray(), title);
        //        }

        //    }
        //    return new Position(title);
        //}
    }

}
