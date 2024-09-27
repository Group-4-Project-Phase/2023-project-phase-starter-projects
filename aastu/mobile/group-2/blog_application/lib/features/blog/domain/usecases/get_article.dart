import 'package:blog_application/core/exceptions/Failure.dart';
import 'package:blog_application/features/blog/domain/entities/article.dart';
import 'package:blog_application/features/blog/domain/repositories/article_repository.dart';
import 'package:dartz/dartz.dart';
import 'package:equatable/equatable.dart';

import '../../../../core/usecases/usecase.dart';

class GetArticle extends UseCase<Article, GetArticleParams> {
  final ArticleRepository repository;
  GetArticle(this.repository);

  @override
  Future<Either<Failure, Article>> call(GetArticleParams param) async {
    return await repository.getArticle(param.id);
  }
}

class GetArticleParams extends Equatable {
  final String id;
  const GetArticleParams(this.id);

  @override
  List<Object?> get props => [id];
}
