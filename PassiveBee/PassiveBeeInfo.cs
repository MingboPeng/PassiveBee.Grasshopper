using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace PassiveBee
{
    public class PassiveBeeInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "PassiveBee";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("d8b840f1-c769-4ff8-afc7-143b328a3f16");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
