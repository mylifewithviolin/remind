namespace remind.target.mind;

public partial class TargetMind
{
            /// <summary>サブメイン</summary>
        /// <param name="soruce">ソース</param>
        /// <param name="targetSource">ターゲットソース</param>
        public  static int SubMain(string soruce,out string targetSource)
        {
            targetSource=string.Empty;
            // ・int 戻り値
            // □リスト型<ソース行型> ソース行リスト =リスト型<ソース行型>で初期化
            // □リスト型<ターゲットソース行型> ターゲットソース行リスト =リスト型<ターゲットソース行型>で初期化
            int ret=1;
            var sourcesLineList = new List<SourceLines>();
            var targetSourcesLineList = new List<TargetSourceLines>();
            var nameDictionary = new Dictionary<string, string>();
            //名称辞書を初期化する
            initNameDictionary(nameDictionary);

            //ノードカテゴライズする
            ret= nodeCategorized(soruce,sourcesLineList);
            if(ret!=0){
                Console.WriteLine( Messages.E00005);
                return ret;
            }
            sourceslineStringsOrgListOutput(sourcesLineList);//デバッグ出力 ソースコードオリジナル
            //名称辞書を収集する
            ret= collectNameDictionary(sourcesLineList,nameDictionary);
            if(ret!=0){
                Console.WriteLine(Messages.E00006);
                return ret;
            }
            sourcesLineListOutput(sourcesLineList);//デバッグ出力 ソースコード分析結果
            nameDictionaryOutput(nameDictionary);//デバッグ出力 名称辞書

            //先頭の行から順にトランスコンパイルしてターゲットソース行リストを参照で返す
            ret=transcompile(sourcesLineList,targetSourcesLineList,nameDictionary);
            if(ret!=0){
                Console.WriteLine(Messages.E00007);
                return ret;
            }


            //先頭の行から順にコード出力
            targetSource = codeOutput(targetSourcesLineList,sourcesLineList);

            // □戻り値 を返す
            return ret;
        }
}