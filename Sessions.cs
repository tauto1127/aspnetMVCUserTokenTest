using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using webUserLoginTest.Models;

namespace webUserLoginTest;

public class Sessions
{
    private static Dictionary<byte[], Session> _sessions;
    private static Random _random;
    
    /*
     * sessionidの算出方法
     * 算出したdatetime + ユーザーID + パスワード + ランダム数字をハッシュ関数に投げる
     */
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns>セッションid</returns>
    public static byte[] Add(User user)
    {
        byte[] sessionId;
        DateTime now = DateTime.Now;
        using (var hmac = SHA256.Create())
        {
            sessionId = hmac.ComputeHash(Encoding.Unicode.GetBytes(now.ToString() + user.Id + " " + _random.Next()));
        }
        _sessions.Add(sessionId, new Session(
            userId: user.Id, expiration: now.AddMinutes(2), acquisition: now));
        
        return sessionId;
    }

    public static int GetUserId(byte[] sessionId)
    {
        if (!_sessions.ContainsKey(sessionId)) return -1;
        Session session = _sessions[sessionId];
        if (session.expiration < DateTime.Now)
        {
            _sessions.Remove(sessionId);
            return -1;
        };

        return session.userId;
    }
}