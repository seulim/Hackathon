using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMKT.Framework.EnterpriseServices;
using System.Data;
using GMKT.Framework.Data;

namespace GMKT.GMobile.Data
{
    public class GameListDac : MicroDacBase
    {
        /// <summary>
        /// 프로야구 게임 조회
        ///</summary>
        ///<param name="gdlc_cd">카테고리대분류</param>
        ///<param name="gdmc_cd">카테고리중분류</param>
        ///<param name="gdsc_cd">카테고리소분류</param>
        ///<param name="page_size">총데이터COUNT</param>
        ///<param name="page_no">페이지번호</param>
        ///<param name="play_dt">경기시작일</param>
        ///<param name="team_cd">팀코드</param>
        ///<returns></returns>
        public List<BaseBallGameT> SelectBaseBallList(string playdt, string teamcd, int pagesize)
        {
            return MicroDacHelper.SelectMultipleEntities<BaseBallGameT>("ticket_read"
                , "dbo.up_gmkt_front_baseball_YONHGLEE"
                , MicroDacHelper.CreateParameter("@gdlc_cd", "100000059", SqlDbType.VarChar, 10)
                , MicroDacHelper.CreateParameter("@gdmc_cd", "200002138", SqlDbType.VarChar, 10)
                , MicroDacHelper.CreateParameter("@gdsc_cd", string.Empty, SqlDbType.VarChar, 10)
                , MicroDacHelper.CreateParameter("@page_no", 1, SqlDbType.Int, 4)
                , MicroDacHelper.CreateParameter("@page_size", pagesize, SqlDbType.Int, 4)
                , MicroDacHelper.CreateParameter("@play_dt", playdt, SqlDbType.VarChar, 10)
                , MicroDacHelper.CreateParameter("@team_cd", teamcd, SqlDbType.VarChar, 10) 
                );
        }
    }
}
