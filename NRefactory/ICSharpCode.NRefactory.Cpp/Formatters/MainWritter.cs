﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.NRefactory.Cpp.Formatters;

namespace ICSharpCode.NRefactory.Cpp
{
    public class MainWritter
    {
        public static void GenerateMain(string entryType, string entryNamespace, bool inputArgs)
        {
            StreamWriter writer = new StreamWriter(FileWritterManager.WorkingPath + "main.cpp");

            writer.WriteLine("#include \"" + entryType + ".h\"");
            writer.WriteLine("using namespace System;");
            if (!String.IsNullOrEmpty(entryNamespace))
                writer.WriteLine("using namespace " + entryNamespace + ";");
            writer.WriteLine();
            writer.WriteLine("int main(int argc, char *argv[])");
            writer.WriteLine("{");
            writer.WriteLine("\tGC::Init();");
            writer.WriteLine("\tString *args = new String[argc];");
            writer.WriteLine("\tfor(int i = 0; i < argc; i++)");
            writer.WriteLine("\t\targs[i] = argv[i];");
            writer.WriteLine();
            writer.WriteLine("\t" + entryType + "::Main(" + (inputArgs ? "&args" : "") + ");");
            writer.WriteLine("\tGC::Collect();");
            writer.Write("}");

            writer.Flush();
            writer.Close();

            FileWritterManager.AddSourceFile("main.cpp");
        }
    }
}