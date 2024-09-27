import 'package:flutter/material.dart';

class ArticleBody extends StatelessWidget {
  final ImageProvider<Object> image;
  final String article_body;

  const ArticleBody({Key? key, required this.article_body, required this.image})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Container(
            height: 200,
            margin: const EdgeInsets.only(top: 10, bottom: 20),
            width: double.infinity,
            decoration: BoxDecoration(
                image: DecorationImage(image: image, fit: BoxFit.fill),
                borderRadius: const BorderRadius.only(
                    topLeft: Radius.circular(28),
                    topRight: Radius.circular(28))),
          ),
          Container(
            padding: const EdgeInsets.symmetric(horizontal: 10),
            decoration: const BoxDecoration(
              gradient: LinearGradient(
                begin: Alignment(0.00, -1.00),
                end: Alignment(0, 1),
                colors: [Color(0x00F9FAFF), Color(0xFFFAFBFF)],
              ),
            ),
            child: Padding(
              padding: EdgeInsets.all(10),
              child: Text(
                article_body,
                style: const TextStyle(
                  color: Color(0xFF2D4379),
                  fontSize: 16,
                  fontFamily: 'Poppins',
                  fontWeight: FontWeight.w400,
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
