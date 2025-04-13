namespace remind.target.mind;

public partial class TargetMind
{
    
        /// <summary>ノードカテゴライズする</summary>
        /// <param name="soruce">ソース</param>
        /// <param name="sourcesLineList">ソースライン型リスト</param>
        /// <param name="nameDictionary">名称辞書</param>
        private static int nodeCategorized(string source,List<SourceLines> sourcesLineList){
            int ret=1;//初期値 1:失敗
            var lineStrings = source.Split("\r\n");
            for(int i=0;i<lineStrings.Length;i++){
                var sourceLines =new SourceLines();
                var sourceLineString=lineStrings[i];
                sourceLines.sourceLineNumber=i+1;
                sourceLines.lineStringsOrg=sourceLineString;
                sourceLineString=sourceLineString.Trim();
                sourceLines.indentOrg=sourceLines.lineStringsOrg.Substring(0,sourceLines.lineStringsOrg.Length-sourceLineString.Length);
                sourceLines.nodeKind=JugdeSourceLineNode(ref sourceLineString);//ノード種類を処理する
                if(sourceLines.nodeKind==NodeKind.ND_EXCEPTION){
                    Console.WriteLine(sourceLines.lineStringsOrg);                   
                    return ret;
                } 
                sourceLines.lineStrings=sourceLineString;
                sourcesLineList.Add(sourceLines);

            }
            ret=0;//ここまで進行したら成功
            return ret;
        }

        /// <summary>ノード種類を処理する</summary>
        /// <param name="soruce">ソース文字列参照</param>
        /// <remarks>備考系ノードは先頭ノードそのまま</remarks>
        private static NodeKind JugdeSourceLineNode(ref string sourceLineString){
            var nodeKind =new NodeKind();
            if(sourceLineString.IndexOf(SourceLineNodeKind.DefBeginNote)==0){
                nodeKind=NodeKind.ND_DEF_BEGIN;//定義開始
                sourceLineString=sourceLineString.Substring(SourceLineNodeKind.DefBeginNote.Length);
            }else if(sourceLineString.IndexOf(SourceLineNodeKind.DefEndNote)==0){
                nodeKind=NodeKind.ND_DEF_END;//定義終了
                sourceLineString=sourceLineString.Substring(SourceLineNodeKind.DefEndNote.Length);

            }else if(sourceLineString.IndexOf(SourceLineNodeKind.DclBeginNote)==0){
                nodeKind=NodeKind.ND_STM_BEGIN;//宣言開始
                sourceLineString=sourceLineString.Substring(SourceLineNodeKind.DclBeginNote.Length);

            }else if(sourceLineString.IndexOf(SourceLineNodeKind.ExcBeginNote)==0){
                nodeKind=NodeKind.ND_EXE_BEGIN;//実行開始
                sourceLineString=sourceLineString.Substring(SourceLineNodeKind.ExcBeginNote.Length);

            // }else if(sourceLineString.IndexOf(SourceLineNodeKind.QutBeginNote)==0){
            //     nodeKind=NodeKind.ND_QAT_BEGIN;//引用開始
            //     sourceLineString=sourceLineString.Substring(SourceLineNodeKind.QutDefBeginNote.Length);

            }else if(sourceLineString.IndexOf(SourceLineNodeKind.QutDefBeginNote)==0){
                nodeKind=NodeKind.ND_QAD_BEGIN;//引用定義開始
                sourceLineString=sourceLineString.Substring(SourceLineNodeKind.QutDefBeginNote.Length);

            }else if(sourceLineString.IndexOf(SourceLineNodeKind.QutDefMidNote)==0){
                nodeKind=NodeKind.ND_QAD_MIDLE;//引用定義中
                sourceLineString=sourceLineString.Substring(SourceLineNodeKind.QutDefMidNote.Length);

            }else if(sourceLineString.IndexOf(SourceLineNodeKind.QutDefEndNote)==0){
                nodeKind=NodeKind.ND_QAD_END;//引用定義終了
                sourceLineString=sourceLineString.Substring(SourceLineNodeKind.QutDefEndNote.Length);

            }else if(sourceLineString.IndexOf(SourceLineNodeKind.SglComentNote)==0){
                nodeKind=NodeKind.ND_SCM_BEGIN;//単行備考開始

            }else if(sourceLineString.IndexOf(SourceLineNodeKind.JavDocComBeginNote)==0){
                nodeKind=NodeKind.ND_JDC_BEGIN;//JavaDoc備考開始

            }else if(sourceLineString.IndexOf(SourceLineNodeKind.JavDocComMidNote)==0){
                if(sourceLineString.IndexOf(SourceLineNodeKind.JavDocComParmNote)>=0){
                    nodeKind=NodeKind.ND_JDC_PARAM;//JavaDoc備考変数
                }else if(sourceLineString.IndexOf(SourceLineNodeKind.JavDocComEndNote)>=0){
                    nodeKind=NodeKind.ND_JDC_END;//JavaDoc備考終了
                }else{
                    nodeKind=NodeKind.ND_JDC_MIDLE;//JavaDoc備考途中
                }

            }else if(sourceLineString== ""){
                nodeKind=NodeKind.ND_EMPTY_LINE;//空改行
            }else{
                nodeKind=NodeKind.ND_EXCEPTION;//例外
            }
            return nodeKind;
        }
}