import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../../../../../core/presentation/theme/app_colors.dart';
import '../../../../article/domain/entities/article.dart';
import '../../../../article/presentation/bloc/bookmark_bloc.dart';

class Bookmark extends StatelessWidget {
  final bool isBookmarked;
  final Article article;

  const Bookmark({
    super.key,
    required this.iconSize,
    required this.isBookmarked,
    required this.article,
  });

  final double iconSize;

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () => _changeBookmarkStatus(context),
      child: Icon(
        isBookmarked ? Icons.bookmark_outlined : Icons.bookmark_border,
        color: AppColors.blue,
        size: iconSize,
      ),
    );
  }

  void _changeBookmarkStatus(BuildContext context) {
    if (isBookmarked) {
      context.read<BookmarkBloc>().add(RemoveFromBookmarkEvent(article.id));
    } else {
      context.read<BookmarkBloc>().add(AddToBookmarkEvent(article));
    }
  }
}
