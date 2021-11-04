(function ($) {
	function get_list(opts){
		console.log(opts)
		for(var i=0;i<opts.count;i++){
			for(var j=0;j<opts.img_arr.length;j++){
				var html = '<div class="con"><div class="image_fra"><img src="'+opts.img_arr[j]['img']+'"></div><div class="name back_overflow">'+opts.img_arr[j]['name']+'</div></div>';
				$(opts.name).find(".draw_prize_item").eq(0).find(".con_list").append(html)
				$(opts.name).find(".draw_prize_item").eq(1).find(".con_list").append(html)
				$(opts.name).find(".draw_prize_item").eq(2).find(".con_list").append(html)
			}
		}
		var height = $(opts.name).find(".draw_prize_item").height();
		$(opts.name).find(".draw_prize_item").eq(1).find(".con_list").css("transform","translate(0px, "+(-height)+"px)");
		$(opts.name).find(".draw_prize_item").eq(2).find(".con_list").css("transform","translate(0px, "+(-height*2)+"px)");
	}
	$.fn.DrawM = function (options) {
	    var opts = {
			count:0,
			img_arr:[],
			get_prize:0,
			get_prize_index:0,
			name:"",
			btn:""
	    };
	    $.extend(opts, options);
		get_list(opts)
		var drawing = 0;
		var list_height = $(opts.name).find(".draw_prize_item .con_list").height();
		var item_height = $(opts.name).find(".draw_prize_item").height();
		list_height = list_height - item_height;
		$(options.btn).click(function(){
			if(drawing == 1){
				return;
			}
			drawing = 1;
			$(".point").css("opacity","0").css("transition-duration","0.5s");;
			
			//是否中奖
			var ori_arr = JSON.parse(JSON.stringify(opts.img_arr));
			for(var i=0;i<ori_arr.length;i++){
				ori_arr[i]['index'] = i;
			}
			
			if(opts.get_prize == 0){
				var x = ori_arr.length-1;
				var y = 0;
				var rand = parseInt(Math.random() * (x - y + 1) + y);
				var first_con = ori_arr[rand];
				var second_con = ori_arr[rand];
				var third_con = ori_arr[rand];
			}else{
				var first_con = ori_arr[opts.get_prize_index];
				var second_con = ori_arr[opts.get_prize_index];
				var third_con = ori_arr[opts.get_prize_index];
			}
			
			if(opts.get_prize==0){
				ori_arr.splice(first_con['index'],1)
				var x = ori_arr.length-1;
				var y = 0;
				var rand1 = parseInt(Math.random() * (x - y + 1) + y);
				var rand2 = parseInt(Math.random() * (x - y + 1) + y);
				second_con = ori_arr[rand1];
				third_con = ori_arr[rand2];
			}
			
			//点击事件
			var that = this;
			var height = $(this).height();
			var ori_height = height;
			height = height*0.7;
			
			$(that).css("height",height+"px");
			setTimeout(function(){
				$(that).css("height",ori_height+"px");
			},500)
			var img_length = opts.img_arr.length;
			console.log(first_con)
			console.log(second_con)
			console.log(third_con)
			$(opts.name).find(".draw_prize_item").eq(0).find(".con_list").css("transform","translate(0px, "+(-list_height+((img_length-1-first_con['index'])*item_height))+"px)").css("transition-duration","5s");
			setTimeout(function(){
				$(opts.name).find(".draw_prize_item").eq(1).find(".con_list").css("transform","translate(0px, "+(-list_height+((img_length-1-second_con['index'])*item_height))+"px)").css("transition-duration","5s");
			},500)
			setTimeout(function(){
				$(opts.name).find(".draw_prize_item").eq(2).find(".con_list").css("transform","translate(0px, "+(-list_height+((img_length-1-third_con['index'])*item_height))+"px)").css("transition-duration","5s");
			},1000)
			setTimeout(function(){
				if(opts.get_prize == 0){
					$("#draw_none_box").css("display","block");
				}else{
					$("#draw_get_box .prize_img img").attr("src",first_con['img']);
					$("#draw_get_box .prize_name span").text(first_con['name']);
					$("#draw_get_box").css("display","block");
				}
			},6500)
			
		})
		
		}
})(jQuery);