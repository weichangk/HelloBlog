using Blog.Application.Blog.Dtos;
using FluentValidation;

namespace Blog.Application.Blog.Validators
{
    public class LeavemsgValidator : AbstractValidator<CommentInputDto>
    {
        public LeavemsgValidator()
        {
            RuleFor(x => x.Content).NotEmpty().MaximumLength(500).WithMessage("留言内容限制1-500个字符");
        }
    }
}