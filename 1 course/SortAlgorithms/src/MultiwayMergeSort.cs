﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sorts
{
    public class MultywayMerge
    {
        public int ways { get; private set; }

        int[] items;

        public MultywayMerge(int ways)
        {
            this.ways = ways;
        }

        public void MainSort(int[] array)
        {
            this.items = array;
            Sort(0, array.Length - 1);
        }

        private void Sort(int start, int end)
        {
            if (start < end)
            {
                int length = (int)Math.Ceiling((double)(end - start + 1) / ways);

                for (int i = 0; i < ways - 1; i++)
                {
                    int l, r;
                    l = start + length * i;
                    r = l + length - 1;
                    Sort(l, r);
                }
                Sort(start + length * (ways - 1), end);

                Merge(start, end, length);
            }
        }

        private void Merge(int start, int end, int length)
        {
            List<List<int>> mergingLists = new List<List<int>>(ways);
            List<int> tempList;
            int[] tempArray = new int[end - start + 1];
            int left, right, k;

            for (int i = 0; (i < ways - 1) & (i < tempArray.Length); i++)
            {
                tempList = new List<int>(length);
                left = start + length * i;
                right = left + length - 1;
                for (k = left; k <= right; k++)
                    tempList.Add(items[k]);
                mergingLists.Add(tempList);
            }

            right = start + length * (ways - 1);
            if (right <= end)
            {
                tempList = new List<int>(end - right + 1);
                for (k = right; k <= end; k++)
                    tempList.Add(items[k]);
                mergingLists.Add(tempList);
            }

            k = 0;
            while (mergingLists.Count > 0)
            {
                int min = 0;
                for (int i = 1; i < mergingLists.Count; i++)
                {
                    if (mergingLists[min][0] > mergingLists[i][0])
                        min = i;
                }
                tempArray[k] = mergingLists[min][0];
                mergingLists[min].RemoveAt(0);
                if (mergingLists[min].Count == 0)
                    mergingLists.RemoveAt(min);
                ++k;
            }

            k = 0;
            for (int i = start; i <= end; i++)
            {
                items[i] = tempArray[k];
                k++;
            }
        }
    }
}