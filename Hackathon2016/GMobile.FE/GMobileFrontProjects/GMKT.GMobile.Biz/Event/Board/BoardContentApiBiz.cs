using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.GMobile.Data;

namespace GMKT.GMobile.Biz
{
    public class BoardContentApiBiz
    {
        public DataAndSize<IEnumerable<BoardContentEntityT>> GetBy(BoardContentSearch search)
        {
            return new BoardContentApiDac().FindBy(search);
        }

        public BoardContentResultCode Add(BoardContentEntityT model)
        {
            return new BoardContentApiDac().Insert(model);
        }

        public BoardContentResultCode Remove(BoardContentEntityT model)
        {
            return new BoardContentApiDac().Delete(model);
        }
    }
}
