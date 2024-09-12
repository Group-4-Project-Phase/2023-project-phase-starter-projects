import 'package:flutter/material.dart';
import 'package:flutter_screenutil/flutter_screenutil.dart';

import '../../../../../core/presentation/theme/app_colors.dart';

class UserBio extends StatelessWidget {
  final String userInfo;

  const UserBio({
    Key? key,
    required this.userInfo,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    ScreenUtil.init(context);

    double titleFontSize = 16.sp;
    double textFontSize = 14.sp;
    double titleSpacing = 11.h;
    double verticalPadding = 24.0.h;
    double bottomSpacing = 20.0.h;
    double cardMarginRight = 20.w;

    return Column(
      children: [
        Container(
          alignment: Alignment.centerLeft,
          padding: EdgeInsets.fromLTRB(0, verticalPadding, 0, 0),
          child: Text(
            'About me',
            textAlign: TextAlign.start,
            style: TextStyle(
              fontSize: titleFontSize,
              fontStyle: FontStyle.italic,
              fontWeight: FontWeight.w100,
              color: AppColors.darkerBlue,
            ),
          ),
        ),
        SizedBox(
          height: titleSpacing,
        ),
        Container(
          margin: EdgeInsets.only(
            right: cardMarginRight,
          ),
          child: Text(
            userInfo,
            textAlign: TextAlign.start,
            style: TextStyle(
              fontSize: textFontSize,
              fontStyle: FontStyle.italic,
              fontWeight: FontWeight.w100,
              color: AppColors.darkBlue,
            ),
          ),
        ),
        SizedBox(
          height: bottomSpacing,
        ),
      ],
    );
  }
}
