import 'package:blog_application/features/blog/presentation/widgets/author_bar.dart';
import 'package:flutter/material.dart';
import 'package:blog_application/features/blog/presentation/widgets/article_reading_title.dart';
import 'package:blog_application/features/blog/presentation/widgets/article_body.dart';

class ArticleReading extends StatefulWidget {
  final int likes;
  const ArticleReading({super.key, required this.likes});

  _ArticleReadingState createState() => _ArticleReadingState();
}

class _ArticleReadingState extends State<ArticleReading> {
  int _likes = 0;

  @override
  void _increment() {
    setState(() {
      _likes += 1;
    });
  }

  void initState() {
    super.initState();
    _likes = widget.likes;
  }

  @override
  Widget build(BuildContext context) {
    String articleText =
        'That marked a turnaround from last year, when the social network reported a decline in users for the first time.\n\nThe drop wiped billions from the firm\'s market value.\n\nSince executives disclosed the fall in February, the firm\'s share price has nearly halved.\nBut shares jumped 19% in after-hours trade on Wednesday.\n\n\n fglfkg;sfkgsg;sggs;gkfssfgkfs;gkfs;gfskgfs \n\n\n sflsfgjlskfgjfdkl';

    return Scaffold(
      appBar: AppBar(
        elevation: 0.0,
        backgroundColor: Colors.white,
        leading: Padding(
          padding: EdgeInsets.only(left: 25),
          child: IconButton(
            onPressed: () => Navigator.pop(context),
            icon: const Icon(Icons.chevron_left),
            color: Colors.black,
          ),
        ),
        actions: const [
          Padding(
            padding: EdgeInsets.symmetric(horizontal: 25, vertical: 10),
            child: Icon(
              Icons.more_horiz,
              color: Colors.black,
            ),
          )
        ],
      ),
      floatingActionButton: ElevatedButton(
          onPressed: () => _increment(),
          child: Row(
            mainAxisSize: MainAxisSize.min,
            children: [
              const Icon(Icons.thumb_up_off_alt_outlined),
              Text('$_likes')
            ],
          )),
      body: Column(children: [
        Padding(
          padding: const EdgeInsets.symmetric(horizontal: 30),
          child:
              Column(crossAxisAlignment: CrossAxisAlignment.center, children: [
            const ArticleReadingTitle(
              title: 'Four Things Everyone Needs To Know',
            ),
            Container(
              margin: const EdgeInsets.symmetric(vertical: 10),
              child: const AuthorBar(
                  image: AssetImage("./assets/images/author_image.png"),
                  name: 'Richard Gervain',
                  time: '2m ago',
                  isBookmarked: true),
            ),
          ]),
        ),
        Expanded(
          child: ArticleBody(
              article_body: articleText,
              image: const AssetImage('assets/images/article_image.png')),
        ),
      ]),
    );
  }
}
