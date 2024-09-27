import 'package:blog_application/core/exceptions/Failure.dart';
import 'package:blog_application/features/blog/domain/entities/article.dart';
import 'package:blog_application/features/blog/domain/repositories/article_repository.dart';
import 'package:dartz/dartz.dart';

class GetBookMarkedArticleUseCase {
  final ArticleRepository repository;
  GetBookMarkedArticleUseCase(this.repository);

  Future<bool> call(String id) async {
    return repository.isArticleBookmarked(id);
  }
}
