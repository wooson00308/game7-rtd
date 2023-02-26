﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gpm.Ui.Internal
{
    [Serializable]
    public class TabGroup : ISerializationCallbackReceiver
    {
        private TabController controller;
        public List<TabLink> list = new List<TabLink>();

        public void Initialize(TabController controller)
        {
            this.controller = controller;
            foreach (TabLink link in list)
            {
                link.Link(this, false);
            }
        }

        public void Add(Tab tab, TabPage page)
        {
            list.Add(new TabLink(this, tab, page));
        }

        public void Remove(int index)
        {
            if (index >= 0 && index < list.Count)
            {
                TabLink link = list[index];

                link.UnLink();
                list.RemoveAt(index);
            }
        }

        public TabController GetController()
        {
            return controller;
        }

        public bool Check(TabGroup group)
        {
            if (this == group ||
                controller == group.controller)
            {
                return true;
            }
            return false;
        }

        public void Release(TabLinkObject obj)
        {
            foreach (TabLink link in list)
            {
                if (link.GetTab() == obj)
                {
                    link.tab = null;
                }

                if (link.GetPage() == obj)
                {
                    link.page = null;
                }
            }

            if (obj.GetGroup() == this)
            {
                obj.root.group = null;
            }
        }

        public void Set(Tab tab, TabPage page)
        {
            TabLink link = Get(tab);
            if(link == null)
            {
                Add(tab, page);
            }
            else
            {
                link.SetPage(page);
            }
        }

        public TabLink Get(Tab tab)
        {
            foreach (TabLink link in list)
            {
                if (link.GetTab() == tab)
                {
                    return link;
                }
            }

            return null;
        }

        public bool Contain(Tab tab)
        {
            return Get(tab) != null;
        }

        public int GetLinkCount(TabLinkObject obj)
        {
            int count = 0;
            foreach (TabLink link in list)
            {
                if (link.GetTab() == obj)
                {
                    count++;
                }

                if (link.GetPage() == obj)
                {
                    count++;
                }
            }

            return count;
        }

        public bool IsLinked(TabLinkObject obj)
        {
            foreach (TabLink link in list)
            {
                if (link.GetTab() == obj)
                {
                    return true;
                }

                if (link.GetPage() == obj)
                {
                    return true;
                }
            }

            return false;
        }

        public void Select(Tab selectTab)
        {
            controller.Select(selectTab);
        }

        public void UpdateLink()
        {
            foreach (TabLink link in list)
            {
                link.Link(this);
            }
        }
        
        public void CheckLink()
        {
            foreach (TabLink link in list)
            {
                link.Link(this, false);
            }

            list.RemoveAll((link) =>
            {
                return link.IsLinked() == false;
            });
        }
        
        public void OnAfterDeserialize()
        {
            CheckLink();
        }

        public void OnBeforeSerialize()
        {
        }
    }
}

