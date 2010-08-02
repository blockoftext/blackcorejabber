using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCoreJabber
{
    class XMLWriter
    {
        StringBuilder sb = new StringBuilder();     

       // bool elementClosed = true;
        bool tagClosed = true;

        public void WriteStartElement(string element)
        {
            if (!tagClosed)
            {
                this.closeTag(false);
             
            }

            sb.Append("<");
            sb.Append(element);
            tagClosed = false;
          //  elementClosed = false;
        }
        public void WriteProperty(string property, string value)
        {
            if (tagClosed)
            {
                return;
            }

            sb.Append(" ");
            sb.Append(property);
            sb.Append("='");
            sb.Append(value);
            sb.Append("'");

        }
        public void WriteString(string element)
        {
            if (!tagClosed)
            {
                this.closeTag(false);
           
            }
            sb.Append(element);
        }
        public void WriteEndElement(string element)
        {
            if (!tagClosed)
            {
                this.closeTag(false);
            
            }
            sb.Append("</");
            sb.Append(element);
            sb.Append(">");
            tagClosed = true;
        }

        public void closeTag(bool hasSlash)
        {
            if (hasSlash)
            {
                sb.Append("/>");
            }
            else
            {
                sb.Append(">");
            }
            tagClosed = true;
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}
