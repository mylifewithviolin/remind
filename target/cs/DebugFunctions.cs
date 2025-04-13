namespace remind.target.cs;

public partial class TargetCs
{
    # region デバッグ用出力関数

        //デバッグ用出力関数
    private static void sourceslineStringsOrgListOutput(List<SourceLines> sourcesLineList){
        Console.WriteLine(string.Empty);
        Console.WriteLine(Messages.I00002);
        for(int i=0;i<sourcesLineList.Count;i++){
            var targetSource =sourcesLineList.ElementAt(i);
                Console.WriteLine(targetSource.lineStringsOrg);
        }
    }
    private static void nameDictionaryOutput(Dictionary<string, string> nameDictionary){
        Console.WriteLine(string.Empty);
        Console.WriteLine(Messages.I00003);
        for(int i=0;i<nameDictionary.Count;i++){
            var dic =nameDictionary.ElementAt(i);
                Console.Write("Key:"+dic.Key);
                Console.WriteLine(" Value:"+dic.Value);
        }
    }
    private static void sourcesLineListOutput(List<SourceLines> sourcesLineList){
        Console.WriteLine(string.Empty);
        Console.WriteLine(Messages.I00004);
        for(int i=0;i<sourcesLineList.Count;i++){
            var targetSource =sourcesLineList.ElementAt(i);
                Console.Write(targetSource.lineStrings);
                nodeKindOutput(targetSource.nodeKind);
        }
    }
    private static void nodeKindOutput(NodeKind nodeKind){
            Console.Write("                              ");
            switch(nodeKind){
            case NodeKind.ND_DEF_BEGIN://定義開始
                Console.WriteLine("ND_DEF_BEGIN//定義開始");
                break;
            case NodeKind.ND_DEF_END://定義終了
                Console.WriteLine("ND_DEF_END//定義終了");
                break;
            case NodeKind.ND_QAD_END://引用定義終了
                Console.WriteLine("ND_QAD_END//引用定義終了");
                break;
            case NodeKind.ND_STM_BEGIN://宣言開始
                Console.WriteLine("ND_STM_BEGIN//宣言開始");
                break;
            case NodeKind.ND_EXE_BEGIN://実行開始
                Console.WriteLine("ND_EXE_BEGIN//実行開始");
                break;
            case NodeKind.ND_QAT_BEGIN://引用開始
                Console.WriteLine("ND_QAT_BEGIN//引用開始");
                break;
            case NodeKind.ND_QAD_BEGIN://引用定義開始
                Console.WriteLine("ND_QAD_BEGIN//引用定義開始");
                break;
            case NodeKind.ND_QAD_MIDLE://引用定義中
                Console.WriteLine("ND_QAD_MIDLE//引用定義中");
                break;
            case NodeKind.ND_SCM_BEGIN://単行備考開始
                Console.WriteLine("ND_SCM_BEGIN//単行備考開始");
                break;
            case NodeKind.ND_JDC_BEGIN://JavaDoc備考開始
                Console.WriteLine("ND_JDC_BEGIN//JavaDoc備考開始");
                break;
            case NodeKind.ND_JDC_MIDLE://JavaDoc備考途中
                Console.WriteLine("ND_JDC_MIDLE//JavaDoc備考途中");
                break;
            case NodeKind.ND_JDC_PARAM://JavaDoc備考変数
                Console.WriteLine("ND_JDC_PARAM//JavaDoc備考変数");
                break;
            case NodeKind.ND_JDC_END://JavaDoc備考終了
                Console.WriteLine("ND_JDC_END JavaDoc備考終了");
                break;
            //行レベルカテゴライズ済のため例外は生じない
        }
    }
# endregion
}