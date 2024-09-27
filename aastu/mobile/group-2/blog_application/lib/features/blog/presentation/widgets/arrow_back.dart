import 'package:flutter/material.dart';

class ArrowBackButton extends StatelessWidget {
  final IconData iconData;
  final Color color;
  final Size size;
  final VoidCallback onPressed;

  const ArrowBackButton(
      {super.key,
      required this.iconData,
      required this.color,
      required this.size,
      required this.onPressed});

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: onPressed,
      child: Icon(
        iconData,
        color: color,
        size: size.width,
      ),
    );
  }
}
