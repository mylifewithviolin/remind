namespace remind.target.mind;

public partial class TargetMind
{
       /// <summary>名称辞書を初期化する</summary>
        /// <param name="nameDictionary">名称辞書</param>
        private static void initNameDictionary(Dictionary<string, string> nameDictionary){
        }


        /// <summary>名称辞書を収集する</summary>
        /// <param name="sourcesLineList">ソースライン型リスト</param>
        private static int collectNameDictionary(List<SourceLines> sourcesLineList,Dictionary<string, string> nameDictionary){
            int ret=1;//初期値 1:失敗
            for(int i=0;i<sourcesLineList.Count;i++){
                var sourceLines=sourcesLineList.ElementAt(i);
                var nodeKind =sourceLines.nodeKind; 
                var sourceLineString=sourceLines.lineStrings ?? "";   
                switch(nodeKind){
                    case NodeKind.ND_JDC_BEGIN://JavaDoc備考開始
                        //ソース行を先読みする
                        string key="";
                        string value="";
                        string valueParam="";
                        bool appearValue=false;
                        for(int j=i+1;j<sourcesLineList.Count;j++){
                            var sourceLinesforward=sourcesLineList.ElementAt(j);
                            var nodeKindforward =sourceLinesforward.nodeKind;
                            var sourceLineStringforward=sourceLinesforward.lineStrings ?? "";
                            if(nodeKindforward==NodeKind.ND_JDC_MIDLE && appearValue==false){
                                appearValue=true;
                                value=sourceLineStringforward.Replace(SourceLineNodeKind.JavDocComMidNote,"").Trim();
                            }
                            if(nodeKindforward==NodeKind.ND_DEF_BEGIN && appearValue){
                                key=sourceLineStringforward.Replace("static","").Replace("public","").Replace("private","").Trim();
                                var pos=key.IndexOf(SourceLineNodeKind.ArgBeginNote);
                                if(pos>0){//かっこが出現した場合
                                    key=key.Replace(key.Substring(pos),"");//かっこ開始から右側は除去
                                }
                                pos=key.LastIndexOf(" ");
                                if(pos>0){//空白が出現した場合
                                                   key=key.Substring(pos).Trim();//空白から右側を対象とする
                                }
                                nameDictionary.Add(key,value);
                                break;//ND_DEF_BEGINで必ずbreak
                            }
                            if(nodeKindforward==NodeKind.ND_JDC_PARAM){
                                valueParam=sourceLineStringforward.Replace(SourceLineNodeKind.JavDocComMidNote,"").Trim();
                                valueParam=valueParam.Replace(SourceLineNodeKind.JavDocComParmNote,"").Trim();
                                var keyValue=valueParam.Split(" ");
                                if(keyValue.Length==2){
                                    nameDictionary.Add(keyValue[0],keyValue[1]);
                                }
                            }
                        }
                        break;
                    case NodeKind.ND_JDC_MIDLE://JavaDoc備考途中
                    case NodeKind.ND_JDC_PARAM://JavaDoc備考変数
                    case NodeKind.ND_JDC_END://JavaDoc備考終了
                        break;
                    case NodeKind.ND_QAD_BEGIN://引用定義開始
                        string key2="";
                        string value2="";
                        string valueParam2="";
                        //classが存在したら当該行でkeyValue取得
                        if(sourceLineString.IndexOf("class")>=0){
                            valueParam2=sourceLineString.Replace("static","").Replace("public","").Replace("private","").Replace("class","").Trim();
                            var keyValue=valueParam2.Split(" ");
                            if(keyValue.Length==2){
                                nameDictionary.Add(keyValue[0],keyValue[1]);
                            }
                        }else{
                            //classが存在しなければソース行を先読みする
                             key2=sourceLineString.Replace("static","").Replace("public","").Replace("private","").Trim();
                            var pos=key2.IndexOf(SourceLineNodeKind.ArgBeginNote);
                            if(pos>0){//かっこが出現した場合
                                key2=key2.Replace(key2.Substring(pos),"").Trim();//かっこ開始から右側は除去
                            }
                            var sourceLinesforward=sourcesLineList.ElementAt(i+1);
                            var nodeKindforward =sourceLinesforward.nodeKind;
                            var sourceLineStringforward=sourceLinesforward.lineStrings ?? "";
                            value2=sourceLineStringforward.Replace("static","").Replace("public","").Replace("private","").Trim();
                            pos=value2.IndexOf(SourceLineNodeKind.ArgBeginNote);
                            if(pos>0){//かっこが出現した場合
                                value2=value2.Replace(value2.Substring(pos),"").Trim();//かっこ開始から右側は除去
                            }
                            nameDictionary.Add(key2,value2);
                        }
                        break;
                    //行レベルカテゴライズ済のため例外は生じない
                }
            }
            ret=0;//ここまで進行したら成功
            return ret;
        }

}