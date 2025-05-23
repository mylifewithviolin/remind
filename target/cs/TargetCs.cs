
namespace remind.target.cs;

    public partial class TargetCs
    {
        // ソース行の格納情報
        private class SourceLines{
            public string? lineStrings;
            public string? lineStringsOrg;
            public NodeKind nodeKind;
            public int sourceLineNumber;

            public string? indentOrg;
            // public List<int> tagetSourceNumList;
            // public List<Token> sourceLintTokenList;
        }

        //▽クラス トークン型
        // class Token{
        //     public TokenKind kind;
        //     public int next;
        //     public int val;
        //     public string? str;
        //     public int len;
        // }

        //▽列挙体 トークン種類
       private enum TokenKind{
            TK_NODE,
            TK_RESERVE,
            TK_IDENT,
            TK_NUM,
            TK_STR

        }

        //     ▽クラス ターゲットソース行型
        //         ・string ターゲット行文字列
        //         ・int ターゲットソース行番号
        //         ・int ソースコード行番号
        //     △
        private class TargetSourceLines{
            public string? targetLineStrings;
            public int targetSourceLineNumber;
            public int sourceLineNumber;
        }
        //列挙体  ノード種類
        enum NodeKind{
            ND_DEF_BEGIN,//       定義開始
            ND_DEF_END,//       定義終了
            ND_ARG_BEGIN,//       引数開始
            ND_ARG_END,//       引数終了
            ND_STM_BEGIN,//       宣言開始
            ND_EXE_BEGIN,//       実行開始
            ND_FRK_BEGIN,//       分岐開始
            ND_FRK_BEGIN2,//       分岐開始２
            ND_FRK_END,//       分岐終了
            ND_FRK_END2,//       分岐終了２
            ND_LOP_BEGIN,//       反復開始
            ND_LOP_BEGIN2,//       反復開始２
            ND_LOP_END,//       反復終了
            ND_LOP_END2,//       反復終了２
            ND_QAT_BEGIN,//       引用開始
            ND_QAD_BEGIN,//       引用定義開始
            ND_QAD_MIDLE,//       引用定義中
            ND_QAD_END,//       引用定義終了
            ND_SCM_BEGIN,//       単行備考開始,
            ND_JDC_BEGIN,//       JavaDoc備考開始
            ND_JDC_MIDLE,//       JavaDoc備考途中
            ND_JDC_PARAM,//       JavaDoc備考変数
            ND_JDC_END,//       JavaDoc備考終了
            ND_EMPTY_LINE,//     空改行
            ND_EXCEPTION//      例外（エラー処理用）
        }
        // ソース行のノード種類
        private class SourceLineNodeKind{
            public const string DefBeginNote ="▽";
            public const string DefEndNote ="△";
            public const string ArgBeginNote ="(";
            public const string ArgEndNote =")";
            public const string DclBeginNote ="・";
            public const string ExcBeginNote ="□";
            public const string ForkBeginNote ="◇";
            public const string ForkBeginNote2 ="の場合";
            public const string ForkEndNote ="◇";
            public const string ForkEndNote2 ="ここまで";//Optinal
            public const string LoopBeginNote ="〇";
            public const string LoopBeginNote2 ="繰り返す";
            public const string LoopEndNote ="〇";
            public const string LoopEndNote2 ="ここまで";//Optional
            public const string QutBeginNote ="■インポート";//インポート文
            public const string QutDefBeginNote ="▼";
            public const string QutDefMidNote ="■";
            public const string QutDefEndNote ="▲";
            public const string SglComentNote ="//";
            public const string JavDocComBeginNote ="/**";
            public const string JavDocComMidNote ="*";
            public const string JavDocComParmNote ="@param";
            public const string JavDocComEndNote ="*/";
        }
        // ターゲットソース行のノード種類
        private class TargetSourceLineNodeKind{
            public const string DefBeginNote ="{";
            public const string DefEndNote ="}";
            public const string ArgBeginNote ="(";
            public const string ArgEndNote =")";
            public const string DclBeginNote ="";
            public const string ExcBeginNote ="";
            public const string SglComentNote ="//";
            public const string JavDocComBeginNote ="///";
            public const string JavDocComMidNote ="/// <summary>jp</summary>";
            public const string JavDocComParmNote ="/// <param name=en>jp</param>";
            public const string JavDocComEndNote ="///";
        }


    }
