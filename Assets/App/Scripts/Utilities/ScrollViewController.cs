using UnityEngine.UIElements;

public class ScrollViewController
{
    public ScrollView scrollView;

    public ScrollViewController(ScrollView scrollView)
    {
        this.scrollView = scrollView;

        scrollView.verticalScrollerVisibility = ScrollerVisibility.Hidden;
        scrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;

        scrollView.touchScrollBehavior = ScrollView.TouchScrollBehavior.Elastic;
    }
}
