using System.Text;
using remind.target.cs;
using remind.target.vb;
using remind.target.mind;
using remind.target.java;

namespace remind
{
    public partial class Program
    {
        /// <summary>ターゲット言語</summary>
        class TargetLang{
            /// <summary>Java</summary>
            public const string Java="java"; // Java
            /// <summary>C#</summary>
            public const string CSharp="csp"; // C#
            /// <summary>VB.NET</summary>
            public const string VbNet="vb"; // VB.NET
            /// <summary>Mind /summary>
            public const string Mind="src";   // Mind 
        } 

        /// <summary>メイン</summary>
        /// <param name="args">引数</param>
        static int Main(string[] args)
        {
            int ret=1;
            if (args.Length != 2) {
                Console.WriteLine(Messages.E00001);
                return ret;
            }

            string fileContent ="";
            string fileName = args[0];
            if (!File.Exists(fileName))
            {
                Console.WriteLine(Messages.E00002 + fileName);
                return ret;
            }

            try
            {
                fileContent = File.ReadAllText(fileName,Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine(Messages.E00003 + ex.Message);
                return ret;
            }

            string targetfileName = args[1];
            string targetfileNameExtension="";
            // 最後のドットの位置を取得
            int lastDotIndex = targetfileName.LastIndexOf('.');
            if (lastDotIndex == -1)
            {
                Console.WriteLine(Messages.E00008);
            }
            else
            {
                targetfileNameExtension = targetfileName.Substring(lastDotIndex + 1);
            }
            string targetSource="";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding;
            switch (targetfileNameExtension) {
            case TargetLang.Java:
                encoding = Encoding.UTF8;
               ret= TargetJava.SubMain(fileContent,out targetSource);
               break;
            case TargetLang.CSharp:
                encoding = Encoding.UTF8;
               ret= TargetCs.SubMain(fileContent,out targetSource);
               break;
            case TargetLang.VbNet:
                encoding = Encoding.UTF8;
               ret= TargetVb.SubMain(fileContent,out targetSource);
               break;
            case TargetLang.Mind:
                encoding = Encoding.GetEncoding("shift_jis");
               ret= TargetMind.SubMain(fileContent,out targetSource);
               break;
            default:
               Console.WriteLine(Messages.E00004);
               return ret;
            }

            if(ret==0){
                // 選択されたエンコーディングでファイルに書き込む
                using (FileStream fileStream = new FileStream(targetfileName, FileMode.Create, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(fileStream, encoding)){
                    writer.Write(targetSource);
                }
            }
            return ret;
        }
    }
}