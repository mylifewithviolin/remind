namespace remind.target.mind;

public partial class TargetMind
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
                case NodeKind.ND_STM_BEGIN://宣言開始
                case NodeKind.ND_EXE_BEGIN://実行開始
                    ret=generateTargetSouceLine(sourceLines,targetSourcesLineList,nodeKind,nameDictionary,ref targetSouceLineNumber);
                    break;
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
                targetSourceLines.targetLineStrings=nameDictionaryReplase(nameDictionary,sourceLineString)+"とは";//名称辞書で置換する+定義開始文字追加
                targetSourceLines.targetSourceLineNumber=targetSouceLineNumber;
                targetSourcesLineList.Add(targetSourceLines);
                break;
            case NodeKind.ND_DEF_END://定義終了
                break;
            case NodeKind.ND_STM_BEGIN://宣言開始
            case NodeKind.ND_EXE_BEGIN://実行開始
                targetSouceLineNumber+=1;
                targetSourceLines.sourceLineNumber=sourceLines.sourceLineNumber;
                targetSourceLines.targetLineStrings=replaseArgumentBlock(nameDictionaryReplase(nameDictionary,sourceLineString))+"。";//名称辞書で置換する+読点追加
                targetSourceLines.targetSourceLineNumber=targetSouceLineNumber;
                targetSourcesLineList.Add(targetSourceLines);
                break;
            case NodeKind.ND_SCM_BEGIN://単行備考開始
                targetSouceLineNumber+=1;
                targetSourceLines.sourceLineNumber=sourceLines.sourceLineNumber;
                targetSourceLines.targetLineStrings=sourceLines?.lineStrings?.Replace(SourceLineNodeKind.SglComentNote,TargetSourceLineNodeKind.SglComentNote);//
                targetSourceLines.targetSourceLineNumber=targetSouceLineNumber;
                targetSourcesLineList.Add(targetSourceLines);
                break;
            case NodeKind.ND_JDC_BEGIN://JavaDoc備考開始
                targetSouceLineNumber+=1;
                targetSourceLines.sourceLineNumber=sourceLines.sourceLineNumber;
                targetSourceLines.targetLineStrings=sourceLines?.lineStrings?.Replace(SourceLineNodeKind.JavDocComBeginNote,TargetSourceLineNodeKind.JavDocComBeginNote);//
                targetSourceLines.targetSourceLineNumber=targetSouceLineNumber;
                targetSourcesLineList.Add(targetSourceLines);
                break;
            case NodeKind.ND_JDC_MIDLE://JavaDoc備考途中
                targetSouceLineNumber+=1;
                targetSourceLines.sourceLineNumber=sourceLines.sourceLineNumber;
                targetSourceLines.targetLineStrings=sourceLines?.lineStrings?.Replace(SourceLineNodeKind.JavDocComMidNote,TargetSourceLineNodeKind.JavDocComMidNote);//
                targetSourceLines.targetSourceLineNumber=targetSouceLineNumber;
                targetSourcesLineList.Add(targetSourceLines);
                break;
            case NodeKind.ND_JDC_PARAM://JavaDoc備考変数
                targetSouceLineNumber+=1;
                targetSourceLines.sourceLineNumber=sourceLines.sourceLineNumber;
                targetSourceLines.targetLineStrings=sourceLines?.lineStrings?.Replace(SourceLineNodeKind.JavDocComParmNote,TargetSourceLineNodeKind.JavDocComParmNote);//
                targetSourceLines.targetSourceLineNumber=targetSouceLineNumber;
                targetSourcesLineList.Add(targetSourceLines);
                break;
            case NodeKind.ND_JDC_END://JavaDoc備考終了
                targetSouceLineNumber+=1;
                targetSourceLines.sourceLineNumber=sourceLines.sourceLineNumber;
                targetSourceLines.targetLineStrings=sourceLines?.lineStrings?.Replace(SourceLineNodeKind.JavDocComEndNote,TargetSourceLineNodeKind.JavDocComEndNote);//
                targetSourceLines.targetSourceLineNumber=targetSouceLineNumber;
                targetSourcesLineList.Add(targetSourceLines);
                break;
            }

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

        /// <summary>引数ブロックを置換する</summary>
        /// <param name="sourceLineString">ソース行文字列</param>
        private static string replaseArgumentBlock(string sourceLineString){
            var tempArgumentBlock="";
            var tempSource="";
            var pos=sourceLineString.IndexOf(SourceLineNodeKind.ArgBeginNote);
            if(pos>0){//かっこが出現した場合
                tempSource=sourceLineString.Replace(sourceLineString.Substring(pos),"").Trim();//かっこ開始から右側は除去
                tempArgumentBlock=sourceLineString.Substring(pos);
                tempArgumentBlock=tempArgumentBlock.Replace(SourceLineNodeKind.ArgBeginNote,"").Replace(SourceLineNodeKind.ArgEndNote,"");
                tempArgumentBlock="「"+tempArgumentBlock.Replace("\"","")+"」を";
                sourceLineString=tempArgumentBlock+" "+tempSource;
            }
            return sourceLineString;
        }
}