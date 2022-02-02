using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Calen
{
    public class Calendar : MonoBehaviour
    {
        [SerializeField] private Dropdown yearDD = null;
        [SerializeField] private Dropdown monthDD = null;
        [SerializeField] private Dropdown dayDD = null;
        private Text year = null;
        private Text month = null;
        private Text day = null;

        private int nowyear = DateTime.Now.Year;
        private int nowmonth = DateTime.Now.Month;
        private int nowday = DateTime.Now.Day;


        private void Start()
        {
            year = SetParent(yearDD);
            month = SetParent(monthDD);
            day = SetParent(dayDD);
            SetYear();
            SetNowDate();
        }

        private void SetYear()
        {
            yearDD.ClearOptions();
            List<string> list = new List<string>();
            for (int i = nowyear; nowyear - 10 < i; i--)
            {
                list.Add(i.ToString());
            }
            yearDD.AddOptions(list);
        }

        public string SetDate()
        {
            string date = year.text + "-" + month.text + "-" + day.text;
            return date;
        }

        public void SetDays()
        {
            int Days = DateTime.DaysInMonth(nowyear - yearDD.value, monthDD.value + 1);
            dayDD.ClearOptions();
            List<string> list = new List<string>();
            for (int i = 1; i <= Days; i++)
            {
                list.Add(i.ToString());
            }
            dayDD.AddOptions(list);
        }

        private void SetNowDate()
        {
            yearDD.value = 0;
            monthDD.value = nowmonth - 1;
            dayDD.value = nowday - 1;
        }

        private Text SetParent(Dropdown obj)
        {
            Text txt = obj.transform.Find("Label").gameObject.GetComponent<Text>();
            return txt;
        }
    }
}