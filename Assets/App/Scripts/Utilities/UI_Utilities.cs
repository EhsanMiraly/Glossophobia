using UnityEngine;
using UnityEngine.UIElements;

public class UI_Utilities
{
    public static void Initialize_ScrollView(ScrollView scrollView)
    {
        scrollView.style.width = Length.Percent(100);
        scrollView.style.height = Length.Percent(100);

        scrollView.verticalScrollerVisibility = ScrollerVisibility.Hidden;
        scrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;

        scrollView.touchScrollBehavior = ScrollView.TouchScrollBehavior.Elastic;

        scrollView.style.marginTop = 0;
        scrollView.style.marginRight = 0;
        scrollView.style.marginBottom = 0;
        scrollView.style.marginLeft = 0;

        scrollView.style.paddingTop = 0;
        scrollView.style.paddingRight = 0;
        scrollView.style.paddingBottom = 0;
        scrollView.style.paddingLeft = 0;
    }

    public static void Initialize_ListView(ListView listView)
    {
        listView.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;

        listView.selectionType = SelectionType.None;

        listView.style.marginTop = 0;
        listView.style.marginRight = 0;
        listView.style.marginBottom = 0;
        listView.style.marginLeft = 0;

        listView.style.paddingTop = 0;
        listView.style.paddingRight = 0;
        listView.style.paddingBottom = 0;
        listView.style.paddingLeft = 0;

        Initialize_ScrollView(listView.Q<ScrollView>());
    }

    public static void Initialize_Foldout(Foldout foldout)
    {
        foldout.value = false;
    }

}
