using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bang.Models
{
    public class PageBarInfoModel
    {
        public int RecordCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentIndex { get; set; }
        public int PageCount
        {
            get
            {
                if (PageSize == 0)
                {
                    return 0;
                }
                var count = RecordCount % PageSize;
                return (RecordCount / PageSize + (count > 0 ? 1 : 0));
            }
        }
        public int StartIndex
        {
            get
            {
                if (CurrentIndex <= 3)
                {
                    return 1;
                }
                else
                {
                    return (CurrentIndex - 2);
                }
            }
        }
        public int EndIndex
        {
            get
            {
                var endIndex = StartIndex + 4;
                if (endIndex >= PageCount)
                {
                    return PageCount;
                }
                else
                {
                    return endIndex;
                }
            }
        }
    }
}
