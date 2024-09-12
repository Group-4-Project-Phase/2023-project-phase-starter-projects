import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';
import 'package:image_picker/image_picker.dart';

import '../../../../article/presentation/widgets/snackbar.dart';
import '../../../../authentication/presentation/bloc/auth_bloc.dart';
import '../../../domain/entities/user_data.dart';
import '../../bloc/user_bloc.dart';
import 'user_bio.dart';
import 'user_profile_info.dart';
import 'user_profile_photo.dart';

class UserProfileDetails extends StatelessWidget {
  final UserData user;
  const UserProfileDetails({Key? key, required this.user}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    ScreenUtil.init(context);

    double horizontalPadding = 40.w;
    double topPadding = 19.h;
    double cardMarginLeft = 32.w;
    double cardMarginTop = 19.h;
    double cardBorderRadius = 16.0.w;

    return BlocListener<UserBloc, UserState>(
      listener: (context, state) {
        if (state is UserProfileUpdatedState) {
          showSuccess(context, 'Profile updated successfully');
          final token = context.read<AuthBloc>().authToken;
          context.read<UserBloc>().add(GetUserEvent(token: token));
        }
      },
      child: Container(
        padding: EdgeInsets.fromLTRB(
          horizontalPadding,
          topPadding,
          horizontalPadding,
          0,
        ),
        child: Card(
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(cardBorderRadius),
          ),
          color: Colors.white,
          child: Container(
            margin: EdgeInsets.fromLTRB(
              cardMarginLeft,
              cardMarginTop,
              0,
              0,
            ),
            child: Column(
              children: [
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                  children: [
                    GestureDetector(
                      onTap: () => _uploadImage(context),
                      child: UserProfilePhoto(
                        photoUrl: user.image,
                      ),
                    ),
                    UserProfileInfo(
                      fullName: user.fullName,
                      username: user.email,
                      profession: user.expertise,
                    ),
                  ],
                ),
                UserBio(
                  userInfo: user.bio,
                ),
                SizedBox(
                  height: 32.w,
                )
              ],
            ),
          ),
        ),
      ),
    );
  }

  void _uploadImage(BuildContext context) async {
    final token = context.read<AuthBloc>().authToken;
    final userBloc = context.read<UserBloc>();

    final picker = ImagePicker();

    final pickedFile = await picker.pickImage(source: ImageSource.gallery);

    if (pickedFile != null) {
      userBloc.add(UpdateUserPhotoEvent(
        token: token,
        imagePath: pickedFile.path,
      ));
    }
  }
}
