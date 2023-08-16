import 'package:todo_main_app/core/usecases/usescase.dart';
import 'package:todo_main_app/features/todo/domain/entities/task_list.dart';
import 'package:todo_main_app/features/todo/domain/repositories/task_repository.dart';

class ViewTaskUsecase implements UseCase<Task, int> {
  final TaskRepository repository;

  ViewTaskUsecase(this.repository);

  @override
  Future<Task> call(int taskId) async {
    return await repository.getTaskById(taskId);
  }

  Future<void> updateTaskCompletionStatus(int taskId, bool isCompleted) async {
    final task = await repository.getTaskById(taskId);
    final updatedTask = task.copyWith(isCompleted: isCompleted);
    await repository.updateTask(updatedTask);
  }
}
