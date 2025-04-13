namespace remind.target.mind
{
    public partial class TargetMind
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

        private class TargetSourceLines{
            public string? targetLineStrings;
            public int targetSourceLineNumber;
            public int sourceLineNumber;
        }

        //列挙体  ノード種類
        private enum NodeKind{
              ND_DEF_BEGIN,//       定義開始
            ND_DEF_END,//       定義終了
            ND_ARG_BEGIN,//       引数開始
            ND_ARG_END,//       引数終了
            ND_STM_BEGIN,//       宣言開始
            ND_EXE_BEGIN,//       実行開始
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
            public const string DefBeginNote ="";
            public const string DefEndNote ="";
            public const string ArgBeginNote ="(";
            public const string ArgEndNote =")";
            public const string DclBeginNote ="";
            public const string ExcBeginNote ="";
            public const string SglComentNote ="※";
            public const string JavDocComBeginNote ="///";
            public const string JavDocComMidNote ="/// <summary>jp</summary>";
            public const string JavDocComParmNote ="/// <param name=en>jp</param>";
            public const string JavDocComEndNote ="///";
        }



    


    }
}