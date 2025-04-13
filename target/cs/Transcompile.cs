namespace remind.target.cs;

public partial class TargetCs
{
            /// <summary>トランスコンパイルする</summary>
        /// <param name="soruce">ソース</param>
        /// <param name="sourcesLineList">ソースライン型リスト</param>
        /// <param name="nameDictionary">名称辞書</param>
        private static int transcompile(List<SourceLines> sourcesLineList,List<TargetSourceLines> targetSourcesLineList,Dictionary<string, string> nameDictionary){
            int ret=1;//初期値 1:失敗
            int targetSouceLineNumber=0;

            for(int i=0;i<sourcesLineList.Count;i++){
                SourceLines sourceLines =sourcesLineList.ElementAt(i);
                var nodeKind =sourceLines.nodeKind;
                
                switch(nodeKind){
                case NodeKind.ND_DEF_BEGIN://定義開始
                    ret=generateTargetSouceLine(sourceLines,targetSourcesLineList,nodeKind,nameDictionary,ref targetSouceLineNumber);
                    break;
                case NodeKind.ND_DEF_END://定義終了
                    ret=generateTargetSouceLine(sourceLines,targetSourcesLineList,nodeKind,nameDictionary,ref targetSouceLineNumber);
                    break;
                case NodeKind.ND_QAD_END://引用定義終了
                    break;
                case NodeKind.ND_STM_BEGIN://宣言開始
                case NodeKind.ND_EXE_BEGIN://実行開始
                    ret=generateTargetSouceLine(sourceLines,targetSourcesLineList,nodeKind,nameDictionary,ref targetSouceLineNumber);
                    break;
                case NodeKind.ND_QAT_BEGIN://引用開始
                case NodeKind.ND_QAD_BEGIN://引用定義開始
                    break;
                case NodeKind.ND_SCM_BEGIN://単行備考開始
                case NodeKind.ND_JDC_BEGIN://JavaDoc備考開始
                case NodeKind.ND_JDC_MIDLE://JavaDoc備考途中
                case NodeKind.ND_JDC_PARAM://JavaDoc備考変数
                case NodeKind.ND_JDC_END://JavaDoc備考終了
                    ret=generateTargetSouceLine(sourceLines,targetSourcesLineList,nodeKind,nameDictionary,ref targetSouceLineNumber);
                    break;
                //行レベルカテゴライズ済のため例外は生じない
                }
            }
            ret=0;//ここまで進行したら成功
            return ret;
        }

        /// <summary>ターゲットソース行を生成する</summary>
        /// <param name="sourceLines">ソースライン型変数</param>
        /// <param name="nameDictionary">名称辞書</param>
        private static int generateTargetSouceLine(SourceLines sourceLines,List<TargetSourceLines> targetSourcesLineList,NodeKind nodeKind,Dictionary<string, string> nameDictionary,ref int targetSouceLineNumber){
            int ret=1;//初期値 1:失敗
            var targetSourceLines =new TargetSourceLines();
            var sourceLineString=sourceLines.lineStrings ?? "";
            switch(nodeKind){
            case NodeKind.ND_DEF_BEGIN://定義開始
                targetSouceLineNumber+=1;
                targetSourceLines.sourceLineNumber=sourceLines.sourceLineNumber;
                targetSourceLines.targetLineStrings=nameDictionaryReplase(nameDictionary,sourceLineString);//名称辞書で置換する
                targetSourceLines.targetSourceLineNumber=targetSouceLineNumber;
                targetSourcesLineList.Add(targetSourceLines);

                targetSouceLineNumber+=1;
                targetSourceLines =new TargetSourceLines();
                targetSourceLines.sourceLineNumber=sourceLines.sourceLineNumber;
                targetSourceLines.targetLineStrings=TargetSourceLineNodeKind.DefBeginNote+Environment.NewLine;//定義開始 中かっこ{を追加
                targetSourceLines.targetSourceLineNumber=targetSouceLineNumber;
                targetSourcesLineList.Add(targetSourceLines);
                break;
            case NodeKind.ND_DEF_END://定義終了
                targetSouceLineNumber+=1;
                targetSourceLines.sourceLineNumber=sourceLines.sourceLineNumber;
                targetSourceLines.targetLineStrings=TargetSourceLineNodeKind.DefEndNote+Environment.NewLine;//定義終了 中かっこ}を追加
                targetSourceLines.targetSourceLineNumber=targetSouceLineNumber;
                targetSourcesLineList.Add(targetSourceLines);
                break;
            case NodeKind.ND_QAD_END://引用定義終了
                break;
            case NodeKind.ND_STM_BEGIN://宣言開始
            case NodeKind.ND_EXE_BEGIN://実行開始
                targetSouceLineNumber+=1;
                targetSourceLines.sourceLineNumber=sourceLines.sourceLineNumber;
                targetSourceLines.targetLineStrings=nameDictionaryReplase(nameDictionary,sourceLineString)+";";//名称辞書で置換し実行文終端;を追加
                targetSourceLines.targetSourceLineNumber=targetSouceLineNumber;
                targetSourcesLineList.Add(targetSourceLines);
                break;
            case NodeKind.ND_QAT_BEGIN://引用開始
                break;
            case NodeKind.ND_QAD_BEGIN://引用定義開始
                break;
            case NodeKind.ND_SCM_BEGIN://単行備考開始
                targetSouceLineNumber+=1;
                targetSourceLines.sourceLineNumber=sourceLines.sourceLineNumber;
                targetSourceLines.targetLineStrings=sourceLines.lineStrings;//単行備考はそのまま
                targetSourceLines.targetSourceLineNumber=targetSouceLineNumber;
                targetSourcesLineList.Add(targetSourceLines);
                break;
            case NodeKind.ND_JDC_BEGIN://JavaDoc備考開始
                // targetSouceLineNumber+=1;
                // targetSourceLines.sourceLineNumber=sourceLines.sourceLineNumber;
                // targetSourceLines.targetLineStrings=sourceLines?.lineStrings?.Replace(SourceLineNodeKind.JavDocComBeginNote,TargetSourceLineNodeKind.JavDocComBeginNote);//
                // targetSourceLines.targetSourceLineNumber=targetSouceLineNumber;
                // targetSourcesLineList.Add(targetSourceLines);
                break;
            case NodeKind.ND_JDC_MIDLE://JavaDoc備考途中
                targetSouceLineNumber+=1;
                targetSourceLines.sourceLineNumber=sourceLines.sourceLineNumber;
                targetSourceLines.targetLineStrings= nameDictionaryJavDocComMidNote(nameDictionary,sourceLineString,TargetSourceLineNodeKind.JavDocComMidNote);
                targetSourceLines.targetSourceLineNumber=targetSouceLineNumber;
                targetSourcesLineList.Add(targetSourceLines);
                break;
            case NodeKind.ND_JDC_PARAM://JavaDoc備考変数
                targetSouceLineNumber+=1;
                targetSourceLines.sourceLineNumber=sourceLines.sourceLineNumber;
                targetSourceLines.targetLineStrings= nameDictionaryJavDocComParmNote(nameDictionary,sourceLineString,TargetSourceLineNodeKind.JavDocComParmNote);
                targetSourceLines.targetSourceLineNumber=targetSouceLineNumber;
                targetSourcesLineList.Add(targetSourceLines);
                break;
            case NodeKind.ND_JDC_END://JavaDoc備考終了
                // targetSouceLineNumber+=1;
                // targetSourceLines.sourceLineNumber=sourceLines.sourceLineNumber;
                // targetSourceLines.targetLineStrings=sourceLines?.lineStrings?.Replace(SourceLineNodeKind.JavDocComEndNote,TargetSourceLineNodeKind.JavDocComEndNote);//
                // targetSourceLines.targetSourceLineNumber=targetSouceLineNumber;
                // targetSourcesLineList.Add(targetSourceLines);
                break;
            }
            //targetSourcesLineList.Add(targetSourceLines);
            ret=0;//ここまで進行したら成功
            return ret;
        } 

        /// <summary>名称辞書でターゲットソース行を置換する</summary>
        /// <param name="nameDictionary">名称辞書</param>
        /// <param name="sourceLineString">ソース行文字列</param>
        private static string nameDictionaryReplase(Dictionary<string, string> nameDictionary,string sourceLineString){
            for(int i=0;i<nameDictionary.Count;i++){
                var dic =nameDictionary.ElementAt(i);
                if(sourceLineString.IndexOf(dic.Key)>=0){
                    sourceLineString=sourceLineString.Replace(dic.Key,dic.Value);
                }
            }
            return sourceLineString;
        }

        /// <summary>名称辞書でJavaDoc備考途中を置換する</summary>
        /// <param name="nameDictionary">名称辞書</param>
        /// <param name="sourceLineString">ソース行文字列</param>
        /// <param name="javDocComMidNote">ソース行文字列</param>
        private static string nameDictionaryJavDocComMidNote(Dictionary<string, string> nameDictionary,string sourceLineString,string javDocComMidNote){
            for(int i=0;i<nameDictionary.Count;i++){
                var dic =nameDictionary.ElementAt(i);
                if(sourceLineString.IndexOf(dic.Value)>=0){
                    sourceLineString=javDocComMidNote.Replace("jp",dic.Key);
                    break;
                }
            }
            return sourceLineString;
        }

        /// <summary>名称辞書でJavaDoc備考変数を置換する</summary>
        /// <param name="nameDictionary">名称辞書</param>
        /// <param name="sourceLineString">ソース行文字列</param>
        /// <param name="javDocComMidNote">ソース行文字列</param>
        private static string nameDictionaryJavDocComParmNote(Dictionary<string, string> nameDictionary,string sourceLineString,string javDocComParmNote){
            for(int i=0;i<nameDictionary.Count;i++){
                var dic =nameDictionary.ElementAt(i);
                if(sourceLineString.IndexOf(dic.Key)>=0){
                    sourceLineString=javDocComParmNote.Replace("jp",dic.Key).Replace("en",dic.Value);
                    break;
                }
            }
            return sourceLineString;
        }
}