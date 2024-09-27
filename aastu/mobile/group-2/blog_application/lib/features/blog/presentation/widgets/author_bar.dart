import 'dart:ui';

import 'package:flutter/material.dart';

class AuthorBar extends StatefulWidget {
  final ImageProvider<Object> image;
  final String name;
  final String time;
  final bool isBookmarked;

  const AuthorBar(
      {super.key,
      required this.image,
      required this.name,
      required this.time,
      required this.isBookmarked});

  _AuthorBarState createState() => _AuthorBarState();
}

class _AuthorBarState extends State<AuthorBar> {
  @override
  Widget build(BuildContext context) {
    return Row(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        Row(children: [
          SizedBox(
            width: 38,
            height: 38,
            child: Container(
              decoration: ShapeDecoration(
                image: DecorationImage(
                  image: widget.image,
                  fit: BoxFit.fill,
                ),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(12),
                ),
              ),
            ),
          ),
          Container(
            margin: const EdgeInsets.symmetric(horizontal: 8),
            child: Column(
              crossAxisAlignment:
                  CrossAxisAlignment.start, // Align text to the left
              children: [
                Text(
                  widget.name,
                  style: const TextStyle(
                    color: Color(0xFF2D4379),
                    fontSize: 14,
                    fontFamily: 'Urbanist',
                    fontWeight: FontWeight.w500,
                  ),
                ),
                const SizedBox(
                  height: 4, // Add some space between the name and time
                ),
                Text(
                  widget.time,
                  style: const TextStyle(
                    color: Color(0xFF7B8BB2),
                    fontSize: 12,
                    fontFamily: 'Poppins',
                    fontWeight: FontWeight.w900,
                  ),
                ),
              ],
            ),
          ),
        ]),
        IconButton(
            onPressed: () {},
            icon: const Icon(
              Icons.bookmark_outline,
              color: Color(0xFF2D4379),
            )),
      ],
    );
  }
}
