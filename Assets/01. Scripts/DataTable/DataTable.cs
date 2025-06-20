// 툴에서 자동으로 생성하는 소스 파일입니다. 수정하지 마세요!
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public partial class DataTable
{
    #region Time
    private ReadOnlyList<TimeData> TimeList = null;
    private ReadOnlyDictionary<int, TimeData> TimeTable = null;

    public ReadOnlyList<TimeData> GetTimeDataList()
    {
        return TimeList;
    }

    public TimeData GetTimeData(int key)
    {
        if (key == 0)
        {
            return null;
        }

        if (TimeTable.TryGetValue(key, out TimeData retVal) == true)
        {
            return retVal;
        }
        else
        {
            Debug.LogError($"Can not find UniqueID of TimeData: <{key}>");
            return null;
        }
    }
    #endregion


    public IEnumerator LoadRoutine()
    {
        int allCount = 0;
        int loadedCount = 0;

        allCount++;
        GetBytes_FromResources("Time", (bytes) =>
        {
            LoadTimeData(bytes);
            loadedCount++;
        });

        yield return new WaitUntil(() => allCount == loadedCount);
    }

    public void LoadForEditor()
    {
        byte[] timeBytes = GetBytes_ForEditor("TimeData");
        LoadTimeData(timeBytes);
    }

    private void LoadTimeData(byte[] bytes)
    {
        List<TimeData> timeList = new List<TimeData>();
        Dictionary<int, TimeData> timeTable = new Dictionary<int, TimeData>();

        Reader = new BinaryReader(new MemoryStream(bytes));

        while (Reader.BaseStream.Position < bytes.Length)
        {
            TimeData data = new TimeData(Reader);
            if (timeTable.ContainsKey(data.TID) == true)
            {
                Debug.LogError("The duplicate TID: " + data.TID + " in Time");
                continue;
            }
            else if (data.TID == 0)
            {
                Debug.LogError("TID is 0 in Time");
                continue;
            }

            timeList.Add(data);
            timeTable.Add(data.TID, data);
        }

        Reader.Close();

        TimeList = new ReadOnlyList<TimeData>(timeList);
        TimeTable = new ReadOnlyDictionary<int, TimeData>(timeTable);
    }
}
