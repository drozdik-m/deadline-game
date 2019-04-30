using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class EditorException : Exception
{
    public EditorException()
    {

    }

    public EditorException(string exceptionMessage)
        :base(exceptionMessage)
    {

    }
}
