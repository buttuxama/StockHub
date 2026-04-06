using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.Comment;
using api.Model;

namespace api.Mapper
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                StockId = comment.StockId,
            };
        }

        public static Comment ToCommentFromCreateDto(
            this CreateCommentRequestDto createCommentDto,
            int stockId
        )
        {
            return new Comment
            {
                Title = createCommentDto.Title,
                Content = createCommentDto.Content,
                StockId = stockId,
            };
        }

        public static Comment ToCommentFromUpdateDto(this UpdateCommentRequestDto updateCommentDto)
        {
            return new Comment
            {
                Title = updateCommentDto.Title,
                Content = updateCommentDto.Content,
            };
        }
    }
}
