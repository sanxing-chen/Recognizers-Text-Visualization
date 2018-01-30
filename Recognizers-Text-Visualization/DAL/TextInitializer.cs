using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Recognizers_Text_Visualization.Models;

namespace Recognizers_Text_Visualization.DAL
{
    public class
        TextInitializer : System.Data.Entity.DropCreateDatabaseAlways<
            TextContext>
    {
        protected override void Seed(TextContext context)
        {
            var texts = new List<Text>
            {
                new Text
                {
                    Content = "我在马路边捡到五毛钱，然后去换了五美元。",
                    Language = Lang.Chinese,
                    Id = 1
                },
                new Text
                {
                    Content = "I got five dollar from my mother.",
                    Language = Lang.English,
                    Id = 2
                },
                new Text
                {
                    Content =
                        "1991年，微軟（Microsoft）創辦人比爾蓋茲（Bill Gates）決定成立一個讓研究員能自由做研究的地方，" +
                        "於是微軟在美國、英國、中國、印度等地設立七個研究院，每年投注約10%年營收的資金在基礎研究上。微軟" +
                        "亞洲研究院（Microsoft Research Asia）因此在1998年誕生，成為僅次於美國總部，人數規模與組織架構" +
                        "都最完整的研究機構。過去19年來，源源不絕的研發動能就從這裡湧現，不僅支援中國本地市場服務，技術更" +
                        "蔓延到全世界：MSRA在國際一流期刊與會議上發表的論文累計超過4千篇，超過400項科研成果已導入微軟產品" +
                        "中，從早期的Windows、Office等核心產品，再到近年Azure、Skype Translator、HoloLens、Cortana和" +
                        "小冰等人工智慧應用。",
                    Language = Lang.Chinese,
                    Id = 3
                }
            };
            texts.ForEach(text => context.Texts.Add(text));
            context.SaveChanges();
        }
    }
}