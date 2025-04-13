namespace remind.target.mind;

public partial class TargetMind
{
    
        /// <summary>コード出力する</summary>
        /// <param name="soruce">ソース</param>
        /// <param name="targetSourcesLineList)">ターゲットソースライン型リスト</param>
        private static string codeOutput(List<TargetSourceLines> targetSourcesLineList,List<SourceLines> sourcesLineList){
            var targetSource=string.Empty;
            Console.WriteLine("");
            Console.WriteLine(Messages.I00001);;
            for(int i=0;i<targetSourcesLineList.Count;i++){
                var targetSourceLine =targetSourcesLineList.ElementAt(i);
                Console.Write(getOriginalIndent(sourcesLineList,targetSourceLine.sourceLineNumber));
                Console.WriteLine(targetSourceLine.targetLineStrings);
                targetSource+=getOriginalIndent(sourcesLineList,targetSourceLine.sourceLineNumber);
                targetSource+=targetSourceLine.targetLineStrings +Environment.NewLine;
            }
            return targetSource;
        }

        private static string getOriginalIndent(List<SourceLines> sourcesLineList,int sourceLineNumber){
            string indentOrg="";
            for(int i=0;i<sourcesLineList.Count;i++){
                var targetSource =sourcesLineList.ElementAt(i);
                if(targetSource.sourceLineNumber==sourceLineNumber){
                    indentOrg=targetSource.indentOrg ?? "";
                    break;
                }
            }
            return  indentOrg;
        }

}